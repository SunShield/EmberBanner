using System;
using System.Collections.Generic;
using EmberBanner.Core.Enums.Battle.Targeting;
using EmberBanner.Core.Models.Actions;
using OerGraph.Runtime.Unity.Data;
using UnityEngine;

namespace EmberBanner.Core.Models.Cards
{
    [Serializable]
    public class CardModel : AbstractModel
    {
        public Sprite Sprite;
        public int Cost;
        public List<ActionModel> Actions = new();
        public int Target;
        public TargetType TargetType => (TargetType)Target;

        public OerGraphAsset GraphAsset;

        public CardModel(string name) => base.Name = name;
    }
}