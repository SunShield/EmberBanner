using EmberBanner.Unity.Battle.Systems.UnitSpotSystem;
using EmberBanner.Unity.Battle.Views.Impl.Units;
using EmberBanner.Unity.Service;
using TMPro;
using UnityEngine;

namespace EmberBanner.Unity.Battle.Systems.Mouseover
{
    public class UnitSpotMouseoverManager : EBMonoBehaviour
    {
        private static UnitSpotMouseoverManager _instance;
        public static UnitSpotMouseoverManager I
        {
            get
            {
                if (_instance == null)
                    _instance = FindObjectOfType<UnitSpotMouseoverManager>();
                return _instance;
            }
        }

        [SerializeField] private TextMeshProUGUI _mouseoveredUnitNameText;

        private UnitSpot _mouseoveredSpot;
        public UnitSpot MouseoveredSpot
        {
            get => _mouseoveredSpot;
            private set
            {
                _mouseoveredSpot = value;
                _mouseoveredUnitNameText.text = MouseoveredUnit != null ? MouseoveredUnit.Model.Name : "-";
            }
        }
        
        public BattleUnitView MouseoveredUnit => MouseoveredSpot != null ? MouseoveredSpot.Unit : null;

        public void SetMouseoverSpot(UnitSpot spot)
        {
            MouseoveredSpot = spot;
            MouseoveredSpot.SetMouseoveredState(true);
        }

        public void UnsetMouseoverSpot()
        {
            MouseoveredSpot.SetMouseoveredState(false);
            MouseoveredSpot = null;
        }
    }
}