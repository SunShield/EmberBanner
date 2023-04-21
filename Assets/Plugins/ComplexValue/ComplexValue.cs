using System;
using System.Collections.Generic;
using Unity.VisualScripting;

namespace Plugins.ComplexValue
{
    public delegate bool ComplexValueFilterDelegate(ValueModifierType modifierType, ComplexValueModifier modification);
    
    /// <summary>
    /// Class calculating a value by the following formula:
    /// FinalBaseFlatValue = BaseValue + Sum(BaseValueAdditions) - Sum(BaseValueReductions)
    /// FinalBaseValue = FinalBaseFlatValue * (1 + (Sum(BaseValuePercentIncreasements) - Sum(BaseValuePercentDecreasements) / 100))
    /// FinalAdditionalValue = FinalBaseValue + Sum(AdditionalValueAdditions) - Sum(AdditionalValueReductions)
    /// FinalValue = FinalAdditionalValue * Prod(MoreMultipliers/100) * Prod(LessMultipliers/100)
    ///
    /// Also supports filtering: custom filter can be passed in calculation to skip some of the modifiers if needed
    /// </summary>
    public class ComplexValue
    {
        public int BaseValue { get; private set; }
        public Dictionary<ulong, ComplexValueModifier> BaseValueFlatAdditions = new();
        public Dictionary<ulong, ComplexValueModifier> BaseValueFlatReductions = new();
        public Dictionary<ulong, ComplexValueModifier> BaseValuePercentIncreasements = new();
        public Dictionary<ulong, ComplexValueModifier> BaseValuePercentDecreasements = new();
        public Dictionary<ulong, ComplexValueModifier> AdditionalValueFlatAdditions = new();
        public Dictionary<ulong, ComplexValueModifier> AdditionalValueFlatReductions = new();
        public Dictionary<ulong, ComplexValueModifier> MoreMultipliers = new();
        public Dictionary<ulong, ComplexValueModifier> LessMultipliers = new();

        public HashSet<string> Tags = new();
        private bool _isAlwaysPositive;
        
        /// <summary>
        /// Storing delegate here helps avoiding unnecessary allocations during multiple calculations
        /// (Not sure if this is really needed)
        /// </summary>
        private ComplexValueFilterDelegate _filter;

        public ComplexValue(bool isAlwaysPositive, int baseValue = default)
        {
            BaseValue = baseValue;
            _isAlwaysPositive = isAlwaysPositive;
        }

        public void SetFilter(ComplexValueFilterDelegate filter) => _filter = filter;
        public void SetBaseValue(int baseValue) => BaseValue = baseValue;

        public int CalculateNewValue(int newValue)
        {
            BaseValue = newValue;
            return CalculateValue(_filter);
        }

        public void AddModifier(ulong id, ValueModifierType type, int magnitude, object payload = null)
        {
            switch(type)
            {
                case ValueModifierType.BaseFlatAdd:
                    BaseValueFlatAdditions.Add(id, new ComplexValueModifier(magnitude, payload));
                    break;
                case ValueModifierType.BaseFlatRemove:
                    BaseValueFlatReductions.Add(id, new ComplexValueModifier(magnitude, payload));
                    break;
                case ValueModifierType.BaseIncreasement:
                    BaseValuePercentIncreasements.Add(id, new ComplexValueModifier(magnitude, payload));
                    break;
                case ValueModifierType.BaseDecreasement:
                    BaseValuePercentDecreasements.Add(id, new ComplexValueModifier(magnitude, payload));
                    break;
                case ValueModifierType.AdditionalFlatAdd:
                    AdditionalValueFlatAdditions.Add(id, new ComplexValueModifier(magnitude, payload));
                    break;
                case ValueModifierType.AdditionalFlatRemove:
                    AdditionalValueFlatReductions.Add(id, new ComplexValueModifier(magnitude, payload));
                    break;
                case ValueModifierType.More:
                    MoreMultipliers.Add(id, new ComplexValueModifier(magnitude, payload));
                    break;
                case ValueModifierType.Less:
                    LessMultipliers.Add(id, new ComplexValueModifier(magnitude, payload));
                    break;
            }
        }

        public void RemoveModifier(ulong id, ValueModifierType type)
        {
            switch(type)
            {
                case ValueModifierType.BaseFlatAdd:
                    BaseValueFlatAdditions.Remove(id);
                    break;
                case ValueModifierType.BaseFlatRemove:
                    BaseValueFlatReductions.Remove(id);
                    break;
                case ValueModifierType.BaseIncreasement:
                    BaseValuePercentIncreasements.Remove(id);
                    break;
                case ValueModifierType.BaseDecreasement:
                    BaseValuePercentDecreasements.Remove(id);
                    break;
                case ValueModifierType.AdditionalFlatAdd:
                    AdditionalValueFlatAdditions.Remove(id);
                    break;
                case ValueModifierType.AdditionalFlatRemove:
                    AdditionalValueFlatReductions.Remove(id);
                    break;
                case ValueModifierType.More:
                    MoreMultipliers.Remove(id);
                    break;
                case ValueModifierType.Less:
                    LessMultipliers.Remove(id);
                    break;
            }
        }

        public void AddTag(string tag) => Tags.Add(tag);
        public void AddTags(List<string> tags) => Tags.AddRange(tags);

        public ComplexValue Clone()
        {
            var newValue = new ComplexValue(_isAlwaysPositive, BaseValue);
            newValue.BaseValueFlatAdditions = new Dictionary<ulong, ComplexValueModifier>(BaseValueFlatAdditions);
            newValue.BaseValueFlatReductions = new Dictionary<ulong, ComplexValueModifier>(BaseValueFlatReductions);
            newValue.BaseValuePercentIncreasements = new Dictionary<ulong, ComplexValueModifier>(BaseValuePercentIncreasements);
            newValue.BaseValuePercentDecreasements = new Dictionary<ulong, ComplexValueModifier>(BaseValuePercentDecreasements);
            newValue.AdditionalValueFlatAdditions = new Dictionary<ulong, ComplexValueModifier>(AdditionalValueFlatAdditions);
            newValue.AdditionalValueFlatReductions = new Dictionary<ulong, ComplexValueModifier>(AdditionalValueFlatReductions);
            newValue.MoreMultipliers = new Dictionary<ulong, ComplexValueModifier>(MoreMultipliers);
            newValue.LessMultipliers = new Dictionary<ulong, ComplexValueModifier>(LessMultipliers);

            return newValue;
        }

        public int CalculateValue(ComplexValueFilterDelegate filter = null)
        {
            var finalBaseFlatValue = CalculateFinalBaseFlatValue(filter);
            var finalBaseValue = CalculateFinalBaseValue(finalBaseFlatValue, filter);
            var finalAdditionalValue = CalculateFinalAdditionalValue(finalBaseValue, filter);
            var finalValue = CalculateFinalValue(finalAdditionalValue, filter);
            return finalValue;
        }

        private int CalculateFinalBaseFlatValue(ComplexValueFilterDelegate filter = null)
        {
            var finalBaseFlatValue = BaseValue;
            var hasFilter = filter != null;

            foreach (var addition in BaseValueFlatAdditions.Values)
            {
                if (hasFilter && !filter(ValueModifierType.BaseFlatAdd, addition)) continue;
                finalBaseFlatValue += addition.Magnitude;
            }
            
            foreach (var reduction in BaseValueFlatReductions.Values)
            {
                if (hasFilter && !filter(ValueModifierType.BaseFlatRemove, reduction)) continue;
                finalBaseFlatValue -= reduction.Magnitude;
            }

            return finalBaseFlatValue;
        }

        private int CalculateFinalBaseValue(int finalBaseFlatValue, ComplexValueFilterDelegate filter = null)
        {
            var hasFilter = filter != null;
            var multiplier = 0f;

            foreach (var increasement in BaseValuePercentIncreasements.Values)
            {
                if (hasFilter && !filter(ValueModifierType.BaseIncreasement, increasement)) continue;
                multiplier += (increasement.Magnitude / 100f);
            }

            foreach (var decreasement in BaseValuePercentDecreasements.Values)
            {
                if (hasFilter && !filter(ValueModifierType.BaseDecreasement, decreasement)) continue;
                multiplier -= (decreasement.Magnitude / 100f);
            }

            return (int)(finalBaseFlatValue * (1 + multiplier));
        }
        
        private int CalculateFinalAdditionalValue(int finalBaseValue, ComplexValueFilterDelegate filter = null)
        {
            var hasFilter = filter != null;
            var value = finalBaseValue;

            foreach (var addition in AdditionalValueFlatAdditions.Values)
            {
                if (hasFilter && !filter(ValueModifierType.AdditionalFlatAdd, addition)) continue;
                value += addition.Magnitude;
            }
            
            foreach (var reduction in AdditionalValueFlatReductions.Values)
            {
                if (hasFilter && !filter(ValueModifierType.AdditionalFlatRemove, reduction)) continue;
                value -= reduction.Magnitude;
            }

            return value;
        }
        
        private int CalculateFinalValue(int finalAdditionalValue, ComplexValueFilterDelegate filter = null)
        {
            var hasFilter = filter != null;
            float floatValue = finalAdditionalValue;

            foreach (var lessMultiplier in LessMultipliers.Values)
            {
                if (hasFilter && !filter(ValueModifierType.Less, lessMultiplier)) continue;
                floatValue *= (1 - lessMultiplier.Magnitude / 100f);
                
                // If we encounter "100% less, no need to calculate further, value will be still 0"
                if (floatValue == 0f) return 0;
            }

            foreach (var moreMultiplier in MoreMultipliers.Values)
            {
                if (hasFilter && !filter(ValueModifierType.More, moreMultiplier)) continue;
                floatValue *= (1 + moreMultiplier.Magnitude / 100f);
            }

            return (int)floatValue;
        }
    }
}