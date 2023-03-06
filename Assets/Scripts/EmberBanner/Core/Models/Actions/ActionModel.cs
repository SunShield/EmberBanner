using System;
using EmberBanner.Core.Enums.Actions;
using EmberBanner.Core.Service.Classes.Collections;
using EmberBanner.Core.Service.Classes.Fundamental;

namespace EmberBanner.Core.Models.Actions
{
    [Serializable]
    public class ActionModel : AbstractModel
    {
        public string RawDescription;
        public int Magnitude;
        public IntSpan ClashingPower = new();
        public IntGrowthRate ClashingPowerGrowthRate = new();
        public int Threshold;
        public int CoinsAmount;
        public bool IsFast;
        public int TargetsCount;
        public ActionType Type;
        public AggressionType AggressionType;
        public DefenseType DefenseType;
        public StringToActionParamModelDictionary Params;

        public ActionModel(string name, ActionType type)
        {
            Name = name;
            Type = type;
            Params = new();
        }
    }
}