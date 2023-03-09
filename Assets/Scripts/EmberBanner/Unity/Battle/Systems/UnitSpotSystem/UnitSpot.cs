using System;
using EmberBanner.Unity.Battle.Systems.Mouseover;
using EmberBanner.Unity.Battle.Systems.Selection;
using EmberBanner.Unity.Battle.Views.Impl.Units;
using EmberBanner.Unity.Service;
using UnityEngine;

namespace EmberBanner.Unity.Battle.Systems.UnitSpotSystem
{
    public class UnitSpot : EBMonoBehaviour
    {
        [SerializeField] private GameObject _mouseoveredGraphics;
        [SerializeField] private GameObject _selectedGraphics;
        
        public BattleUnitView Unit { get; private set; }
        public bool IsFree => Unit == null;

        public void AddUnit(BattleUnitView unit)
        {
            Unit = unit;
            unit.Tran.parent = Tran;
            unit.Tran.localPosition = Vector3.zero;
        }

        public void RemoveUnit()
        {
            Unit = null;
        }

        public void SetMouseoveredState(bool state) => _mouseoveredGraphics.SetActive(state);
        private void OnMouseEnter() => UnitSpotMouseoverManager.I.SetMouseoverSpot(this);
        private void OnMouseExit() => UnitSpotMouseoverManager.I.UnsetMouseoverSpot();

        public void SetSelectedState(bool state) => _selectedGraphics.SetActive(state);
        private void OnMouseDown() => UnitSelectionManager.I.SelectSpot(this);
    }
}