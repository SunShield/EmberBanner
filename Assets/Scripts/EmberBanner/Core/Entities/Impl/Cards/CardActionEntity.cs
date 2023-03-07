using System.Collections.Generic;
using EmberBanner.Core.Enums.Actions;
using EmberBanner.Core.Ingame;
using EmberBanner.Core.Models.Actions;
using Plugins.ComplexValue;

namespace EmberBanner.Core.Entities.Impl.Cards
{
    public class CardActionEntity : AbstractEntity<ActionModel>
    {
        public ComplexValue Magnitude    { get; private set; }
        public ComplexValue Threshold    { get; private set; }
        public ComplexValue CoinsAmount  { get; private set; }
        public ComplexValue TargetsCount { get; private set; }
        
        public Dictionary<string, ComplexValue> FinalIntParams { get; private set; } = new();

        public CardActionEntity(int id, ActionModel model) : base(id, model)
        {
            Magnitude    = new ComplexValue(true, model.Magnitude);
            Threshold    = new ComplexValue(true, model.Threshold);
            CoinsAmount  = new ComplexValue(true, model.CoinsAmount);
            TargetsCount = new ComplexValue(true, model.TargetsCount);
            
            BuildParams(model);
        }

        private void BuildParams(ActionModel model)
        {
            foreach (var paramName in model.Params.Keys)
            {
                var param = model.Params[paramName];
                if (param.Type == ActionParamType.String) continue;

                FinalIntParams.Add(paramName, new ComplexValue(false, param.IntValue));
            }
        }
    }
}