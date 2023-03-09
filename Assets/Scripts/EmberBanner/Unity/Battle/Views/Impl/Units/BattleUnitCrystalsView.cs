using System.Collections.Generic;
using EmberBanner.Unity.Battle.Views.Impl.Units.Crystals;
using EmberBanner.Unity.Service;
using UnityEngine;

namespace EmberBanner.Unity.Battle.Views.Impl.Units
{
    public class BattleUnitCrystalsView : EBMonoBehaviour
    {
        private static float CrystalWidth = 0.612f;
        
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
            for (int i = 0; i < Crystals.Count; i++)
            {
                Crystals[i].Tran.localPosition = new Vector3(-(Crystals.Count / 2f) * CrystalWidth + CrystalWidth / 2 + CrystalWidth * i, 0f, 0f);
            }
        }
    }
}