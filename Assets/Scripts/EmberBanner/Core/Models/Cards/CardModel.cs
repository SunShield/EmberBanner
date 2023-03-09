using System;
using System.Collections.Generic;
using EmberBanner.Core.Enums.Battle;
using EmberBanner.Core.Models.Actions;
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
        public CardMainTargetType MainTarget;

        public CardModel(string name) => base.Name = name;
    }
}