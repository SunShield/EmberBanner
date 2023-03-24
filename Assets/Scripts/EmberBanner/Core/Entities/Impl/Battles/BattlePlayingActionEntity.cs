using System.Collections.Generic;
using EmberBanner.Core.Entities.Impl.Cards;
using EmberBanner.Core.Enums.Actions;
using EmberBanner.Core.Models.Actions;
using EmberBanner.Unity.Battle.Views.Impl.Cards;
using EmberBanner.Unity.Battle.Views.Impl.Units.Crystals;
using Plugins.ComplexValue;
using Random = UnityEngine.Random;

namespace EmberBanner.Core.Ingame.Impl.Battles
{
    /// <summary>
    /// This class is an action actually taking part in playing process
    ///
    /// Is exists only during actions play phase.
    /// </summary>
    public class BattlePlayingActionEntity : CardActionEntity
    {
        /// <summary>
        /// Crystal holding this action
        /// </summary>
        public BattleUnitCrystalView Holder { get; private set; }
        
        /// <summary>
        /// Card current action is related to
        /// </summary>
        public BattleCardView Card => Holder.Card;
        
        /// <summary>
        /// Crystal action is targeting right now
        /// </summary>
        public BattleUnitCrystalView Target { get; set; }

        public bool TargetIsDead => Target.IsDead;
        public bool IsTargetingSelf => Holder == Target;
        
        /// <summary>
        /// Action can be cancelled via various conditions,
        /// but the main one is being unable to target card's target
        ///
        /// some events during gameplay may un-cancel actions
        /// </summary>
        public bool IsCancelled { get; set; }

        /// <summary>
        /// Previous coin results
        /// </summary>
        public List<bool> CoinResults { get; set; } = new();

        /// <summary>
        /// Full amount of coins minus coins already rolled
        /// </summary>
        public int CurrentCoinsAmount => CoinsAmount.CalculateValue() - CoinResults.Count;

        public bool IsFullyResolved => CurrentCoinsAmount == 0;

        /// <summary>
        /// Clashing roll of an action
        /// </summary>
        public int? CurrentRoll { get; set; }

        public BattlePlayingActionEntity(CardActionEntity entity, BattleUnitCrystalView holder) : this(entity.Id, entity.Model)
        {
            Holder = holder;
        }
        
        public BattlePlayingActionEntity(int id, ActionModel model) : base(id, model)
        {
        }

        public void Roll() => CurrentRoll = Random.Range(MinClashingPower.CalculateValue(), MaxClashingPower.CalculateValue() + 1);

        public bool IsSelfPlayable()
        {
            if (IsReactive()) return false;
            if (Model.Type == ActionType.Aggression) return false;
            return true;
        }
        
        public bool IsReactive()
        {
            // Field and Shield defenses are never reactive
            if (Model.Type == ActionType.Defense && (Model.DefenseType == DefenseType.Barrier || Model.DefenseType == DefenseType.Block)) return true;
            
            // Targeted Aggressions are never reactive
            if (Model.Type == ActionType.Aggression && IsTargetingSelf) return true;

            return false;
        }
        
        public bool IsNotReactiveAgainstTarget(BattlePlayingActionEntity initiator)
        {
            // Supports are never reactive
            if (Model.Type == ActionType.Support) return false;
            
            // Field and Shield defenses are never reactive
            if (Model.Type == ActionType.Defense && (Model.DefenseType == DefenseType.Shield || Model.DefenseType == DefenseType.Field)) return true;
            
            // Targeted Aggressions are never reactive
            if (Model.Type == ActionType.Aggression && !IsTargetingSelf) return false;
            
            // Block and Barrier defenses can be reactive only if targeting self or initiator of an attack
            if (Model.Type == ActionType.Defense && (IsTargetingSelf || Target == initiator.Holder))
            {
                // only proper-typed reactive defense actions are reactive against aggression
                // Block reacts on Damage
                // Barrier reacts on Harm
                if (initiator.Model.Type == ActionType.Aggression &&
                    (initiator.Model.AggressionType == AggressionType.Damage && Model.DefenseType == DefenseType.Block ||
                     initiator.Model.AggressionType == AggressionType.Harm && Model.DefenseType == DefenseType.Barrier))
                    return false;
            }
            

            return true;
        }

        public void SetLosingMagnitude()
        {
            Magnitude.AddModifier(0, ValueModifierType.BaseFlatRemove, ClashLoseHandicap.CalculateValue());
        }

        public bool FlipCoin()
        {
            var randomSeed = Random.Range(0, 2);
            var result = randomSeed == 1;
            CoinResults.Add(result);

            return result;
        }

        public void PostSingleResolve()
        {
            Magnitude.RemoveModifier(0, ValueModifierType.BaseFlatRemove);
            CurrentRoll = null;
        }
    }
}