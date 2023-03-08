using EmberBanner.Unity.Battle.Systems.StateSystem;
using EmberBanner.Unity.Battle.Systems.TurnOrder;
using EmberBanner.Unity.Battle.Views.Factories;
using EmberBanner.Unity.Service;
using UnityEngine;

namespace EmberBanner.Unity.Battle.Management
{
    public class BattleManager : EBMonoBehaviour
    {
        private static BattleManager _instance;
        public static BattleManager I
        {
            get
            {
                if (_instance == null)
                    _instance = FindObjectOfType<BattleManager>();
                return _instance;
            }
        }

        [SerializeField] private BattleStructure _structure;
        [SerializeField] private BattleStateController _stateController;
        [SerializeField] private BattleUnitViewFactory _unitViewFactory;
        [SerializeField] private BattleUnitCrystalViewFactory _crystalViewFactory;
        
        public TurnOrderController TurnOrderController { get; private set; } = new();
        public BattleRegistry Registry { get; private set; } = new();
        
        public BattleStateController StateController => _stateController;
        public BattleStructure Structure => _structure;
        public BattleUnitViewFactory UnitViewFactory => _unitViewFactory;
        public BattleUnitCrystalViewFactory CrystalViewFactory => _crystalViewFactory;
    }
}