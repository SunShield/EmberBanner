using System;
using System.Collections.Generic;
using EmberBanner.Core.Enums.Battle;
using EmberBanner.Core.Enums.Battle.Targeting;
using EmberBanner.Core.Models.Actions;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.Serialization;

namespace EmberBanner.Core.Models.Cards
{
    [Serializable]
    public class CardModel : AbstractModel
    {
        public Sprite Sprite;
        public int Cost;
        public List<ActionModel> Actions = new();
        public int Target;
        public CardTargetType TargetType => (CardTargetType)Target;

        public CardModel(string name) => base.Name = name;
    }
}