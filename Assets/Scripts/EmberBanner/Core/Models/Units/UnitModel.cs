using System;
using System.Collections.Generic;
using EmberBanner.Core.Models.Units.Cards;
using EmberBanner.Core.Models.Units.Crystals;
using UnityEngine;

namespace EmberBanner.Core.Models.Units
{
    [Serializable]
    public class UnitModel : AbstractModel
    {
        public int StartingHealth;
        public int MaxHealth;
        public int HealthRegen;
        public int StartingWill;
        public int MaxWill;
        public int WillRegen;
        public int StartingEnergy;
        public int MaxEnergy;
        public int EnergyRegen;
        public int HandSize;
        public int Draw;
        public int PhysicalResistance;
        public int NaturalResistance;
        public int MagicResistance;
        public int WillPhysicalResistance;
        public int WillNaturalResistance;
        public int WillMagicResistance;
        public List<UnitCrystalModel> Crystals = new();

        public bool IsEnemyUnit;
        /// <summary>
        /// Only for units meant to be "enemies" (some of them can still be playable by players)
        /// </summary>
        public List<UnitDefaultCardModel> DefaultCards = new();

        public Sprite Sprite;

        public UnitModel(string name) => Name = name;
    }
}