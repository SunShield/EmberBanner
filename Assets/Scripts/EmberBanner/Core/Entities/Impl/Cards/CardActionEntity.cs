using System.Collections.Generic;
using System.Linq;
using EmberBanner.Core.Enums.Actions;
using EmberBanner.Core.Ingame;
using EmberBanner.Core.Models.Actions;
using Plugins.ComplexValue;

namespace EmberBanner.Core.Entities.Impl.Cards
{
    public class CardActionEntity : AbstractEntity<ActionModel>
    {
        public ComplexValue Magnitude               { get; private set; }
        public ComplexValue ClashLoseHandicap       { get; private set; }
        public ComplexValue Threshold               { get; private set; }
        public ComplexValue CoinsAmount             { get; private set; }
        public ComplexValue TargetsCount            { get; private set; }
        public ComplexValue MinClashingPower        { get; private set; }
        public ComplexValue MaxClashingPower        { get; private set; }
        
        public Dictionary<string, ComplexValue> FinalIntParams { get; private set; } = new();

        public CardActionEntity(int id, ActionModel model) : base(id, model)
        {
            BuildMagnitudeParam();
            ClashLoseHandicap       = new ComplexValue(true,  model.ClashLoseHandicap);
            Threshold               = new ComplexValue(true,  model.Threshold);
            CoinsAmount             = new ComplexValue(true,  model.CoinsAmount);
            TargetsCount            = new ComplexValue(true,  model.TargetsCount);
            MinClashingPower        = new ComplexValue(true,  model.ClashingPower.Min);
            MaxClashingPower        = new ComplexValue(true,  model.ClashingPower.Max);
            
            BuildParams();
        }

        private void BuildParams()
        {
            foreach (var paramName in Model.Params.Keys)
            {
                var paramModel = Model.Params[paramName];
                if (paramModel.Type == ActionParamType.String) continue;

                var paramValue = new ComplexValue(false, paramModel.IntValue);
                paramValue.AddTags(paramModel.TagString.Replace(" ", "")
                                                       .Split(",")
                                                       .ToList());
                FinalIntParams.Add(paramName, paramValue);
            }
        }

        private void BuildMagnitudeParam()
        {
            var magnitudeParam = new ComplexValue(true, Model.Magnitude);
            magnitudeParam.AddTag("Magnitude");
            var secondaryTags = new List<string>();
            var typeTag = "";
            if (Model.Type == ActionType.Aggression)
            {
                typeTag = Model.AggressionType.ToString();
                if (Model.AggressionType == AggressionType.Harm) 
                    secondaryTags = Model.HarmTags.Replace(" ", "").Split(",").ToList();
                    
            }
            else if (Model.Type == ActionType.Defense) typeTag = Model.DefenseType.ToString();
            else if (Model.Type == ActionType.Support)
            {
                typeTag = Model.SupportType.ToString();
                if (Model.SupportType == SupportType.Aid)
                    secondaryTags = Model.AidTags.Replace(" ", "").Split(",").ToList();
            }
            
            if (!string.IsNullOrEmpty(typeTag)) magnitudeParam.AddTag(typeTag);
            if (secondaryTags.Count != 0) magnitudeParam.AddTags(secondaryTags);
            
            Magnitude = magnitudeParam;
        }
    }
}