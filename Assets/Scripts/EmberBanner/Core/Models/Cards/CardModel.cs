using System;
using System.Collections.Generic;
using EmberBanner.Core.Models.Actions;
using ItemsManager.Databases.Elements;
using UnityEngine;

namespace EmberBanner.Core.Models.Cards
{
    [Serializable]
    public class CardModel : AbstractModel, IAbstractDatabaseElement
    {
        public Sprite Sprite;
        public int Cost;
        public List<ActionModel> Actions = new();
        
        public string Name => base.Name;

        public CardModel(string name) => base.Name = name;
    }
}