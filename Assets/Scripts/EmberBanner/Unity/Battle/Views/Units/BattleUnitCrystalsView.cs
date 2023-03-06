using System.Collections.Generic;
using EmberBanner.Unity.Battle.Views.Units.Crystals;
using EmberBanner.Unity.Service;
using UnityEngine;

namespace EmberBanner.Unity.Battle.Views.Units
{
    public class BattleUnitCrystalsView : EBMonoBehaviour
    {
        [SerializeField] private Transform _crystalsOrigin;
        public List<BattleUnitCrystalView> Crystals { get; private set; }

        public void SetCrystals(List<BattleUnitCrystalView> crystals)
        {
            Crystals = crystals;
            foreach (var crystal in Crystals)
            {
                AddCrystal(crystal);
            }
            
            SetCrystalPositions();
        }

        private void AddCrystal(BattleUnitCrystalView crystal)
        {
            crystal.Tran.parent = _crystalsOrigin;
            crystal.Tran.localPosition = Vector3.zero;
            Crystals.Add(crystal);
        }

        private void SetCrystalPositions()
        {
            
        }
    }
}