using System.Collections.Generic;
using EmberBanner.Core.Entities.Impl.Units;
using EmberBanner.Core.Enums.Battle;
using EmberBanner.Core.Models.Units;
using UnityEngine;

namespace EmberBanner.Core.Ingame.Impl.Battles
{
    /// <summary>
    /// In-battle only unit
    ///
    /// Contains some additional battle-related info
    /// </summary>
    public class BattleUnitEntity : UnitEntity
    {
        public int CurrentHealth { get; set; }
        public int CurrentWill   { get; set; }
        public int CurrentEnergy { get; set; }
        public int CurrentShield { get; set; }
        public int CurrentField  { get; set; }
        
        public UnitControllerType Controller { get; private set; }

        public bool IsDead => CurrentHealth == 0;
        public bool IsStaggered => CurrentWill == 0;

        public BattleUnitEntity(int id, UnitModel model) : base(id, model)
        {
        }

        public override void Initialize(object payload)
        {
            Controller = (UnitControllerType)payload;
            CurrentHealth = StartingHealth.CalculateValue();
            CurrentWill = StartingWill.CalculateValue();
            CurrentEnergy = StartingEnergy.CalculateValue();
        }

        #region Cards and Crystals

        public IEnumerable<BattleUnitCrystalEntity> EnumerateCrystals()
        {
            foreach (var crystal in Crystals)
            {
                yield return crystal as BattleUnitCrystalEntity;
            }
        }

        public IEnumerable<BattleCardEntity> EnumerateCards()
        {
            foreach (var card in Deck.Values)
            {
                yield return card as BattleCardEntity;
            }
        }

        public BattleCardEntity GetCard(int index) => Deck[index] as BattleCardEntity;
        public BattleUnitCrystalEntity GetCrystal(int index) => Crystals[index] as BattleUnitCrystalEntity;

        #endregion

        #region Stats

        public void ChangeHealth(int changeMagnitude)
        {
            if (IsDead) return;
            
            CurrentHealth = Mathf.Clamp(CurrentHealth + changeMagnitude, 0, MaxHealth.CalculateValue());
        }

        public void ChangeWill(int changeMagnitude)
        {
            if (IsDead || IsStaggered) return;
            
            CurrentWill = Mathf.Clamp(CurrentWill + changeMagnitude, 0, MaxWill.CalculateValue());
        }
        
        public void ChangeEnergy(int changeMagnitude)
        {
            if (IsDead) return;
            
            CurrentEnergy = Mathf.Clamp(CurrentEnergy + changeMagnitude, 0, MaxEnergy.CalculateValue());
        }
        
        public void ChangeShield(int changeMagnitude)
        {
            if (IsDead) return;
            
            CurrentEnergy = Mathf.Max(CurrentShield + changeMagnitude, 0);
        }
        
        public void ChangeField(int changeMagnitude)
        {
            if (IsDead) return;
            
            CurrentEnergy = Mathf.Max(CurrentField + changeMagnitude, 0);
        }

        #endregion
    }
}