using System;
using System.Collections.Generic;
using UnityEngine;

namespace EmberBanner.Core.Models.Battles
{
    [Serializable]
    public class BattleModel : AbstractModel
    {
        public Sprite Sprite;
        public List<UnitInBattleModel> DeterminedEnemies;
        
        public BattleModel(string name)
        {
            Name = name;
        }
    }
}