﻿using System;
using EmberBanner.Core.Enums.Actions;
using EmberBanner.Core.Enums.Battle.Targeting;
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
        public int PossibleAggressionTargets;
        public int PossibleDefenseTargets;
        public int PossibleSupportTargets;
        public bool IsAoE;
        public StringToActionParamModelDictionary Params;

        public AggressionTargetType AggressionTargetType => (AggressionTargetType)PossibleAggressionTargets;
        public DefenseTargetType DefenseTargetType       => (DefenseTargetType)PossibleDefenseTargets;
        public SupportTargetType SupportTargetType       => (SupportTargetType)PossibleSupportTargets;

        public ActionModel(string name, ActionType type)
        {
            Name = name;
            Type = type;
            Params = new();
        }
    }
}