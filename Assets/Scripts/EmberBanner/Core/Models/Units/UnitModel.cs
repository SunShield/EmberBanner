using System;
using System.Collections.Generic;
using EmberBanner.Core.Models.Units.Crystals;
using UnityEngine;

namespace EmberBanner.Core.Models.Units
{
    [Serializable]
    public class UnitModel : AbstractModel
    {
        public int StartingHealth;
        public int MaxHealth;
        public int StartingEnergy;
        public int MaxEnergy;
        public List<UnitCrystalModel> Crystals;

        public Sprite Sprite;

        public UnitModel(string name) => Name = name;
    }
}