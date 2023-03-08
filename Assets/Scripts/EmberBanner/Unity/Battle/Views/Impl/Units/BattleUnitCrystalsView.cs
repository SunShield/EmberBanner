using System.Collections.Generic;
using EmberBanner.Unity.Battle.Views.Impl.Units.Crystals;
using EmberBanner.Unity.Service;
using UnityEngine;

namespace EmberBanner.Unity.Battle.Views.Impl.Units
{
    public class BattleUnitCrystalsView : EBMonoBehaviour
    {
        [SerializeField] private Transform _crystalsOrigin;
        public List<BattleUnitCrystalView> Crystals { get; private set; } = new();

        public void SetCrystals(List<BattleUnitCrystalView> crystals)
        {
            foreach (var crystal in crystals)
            {
                AddCrystal(crystal);
            }
        }

        private void AddCrystal(BattleUnitCrystalView crystal)
        {
            crystal.Tran.parent = _crystalsOrigin;
            crystal.Tran.localPosition = Vector3.zero;
            Crystals.Add(crystal);
            
            SetCrystalPositions();
        }

        private void SetCrystalPositions()
        {
            
        }
    }
}