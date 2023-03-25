using System;
using EmberBanner.Core.Enums.Actions;
using EmberBanner.Core.Enums.Battle;
using EmberBanner.Core.Enums.Battle.Targeting;
using EmberBanner.Core.Graphs.Card.Action;
using EmberBanner.Core.Service.Classes.Collections;
using EmberBanner.Core.Service.Classes.Fundamental;

namespace EmberBanner.Core.Models.Actions
{
    [Serializable]
    public class ActionModel : AbstractModel
    {
        public string WielderCardName;
        
        public string RawDescription;
        public int Magnitude;
        public IntSpan ClashingPower = new();
        public IntGrowthRate ClashingPowerGrowthRate = new();
        public int ClashLoseHandicap;
        public int Threshold;
        public int CoinsAmount;
        public bool IsFast;
        public int TargetsCount;
        public ActionType Type;
        public AggressionType AggressionType;
        public string HarmTags;
        public DefenseType DefenseType;
        public SupportType SupportType;
        public string AidTags;
        public int PossibleTargets;
        public bool IsAoE;
        public DamageType DamageType;
        public StringToActionParamModelDictionary Params;

        public TargetType TargetType => (TargetType)PossibleTargets;

        public ActionModel(string name, ActionType type)
        {
            Name = name;
            Type = type;
            Params = new();
        }
    }
}