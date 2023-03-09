using EmberBanner.Core.Enums.Battle.States;
using EmberBanner.Unity.Battle.Management;
using EmberBanner.Unity.Battle.Systems.UnitSpotSystem;
using EmberBanner.Unity.Battle.Views.Impl.Units;
using EmberBanner.Unity.Service;
using TMPro;
using UnityEngine;

namespace EmberBanner.Unity.Battle.Systems.Selection
{
    public class UnitSelectionManager : EBMonoBehaviour
    {
        private static UnitSelectionManager _instance;
        public static UnitSelectionManager I
        {
            get
            {
                if (_instance == null)
                    _instance = FindObjectOfType<UnitSelectionManager>();
                return _instance;
            }
        }
        
        [SerializeField] private TextMeshProUGUI _selectedUnitNameText;
        
        private UnitSpot _selectedSpot;
        public UnitSpot SelectedSpot
        {
            get => _selectedSpot;
            private set
            {
                _selectedSpot = value;
                _selectedUnitNameText.text = SelectedUnit != null ? SelectedUnit.Model.Name : "-";
            }
        }
        
        public BattleUnitView SelectedUnit => SelectedSpot != null ? SelectedSpot.Unit : null;

        private void Start()
        {
            BattleManager.I.StateController.onStateChanged += OnStateChanged;
        }

        public void SelectSpot(UnitSpot spot)
        {
            if (BattleManager.I.StateController.State < BattleState.TurnPlan ||
                BattleManager.I.StateController.State > BattleState.TurnEnd) 
                return;
            
            if (spot.Unit == null) return;
            if (SelectedSpot != null) UnselectSpot();
            SelectedSpot = spot;
            SelectedSpot.SetSelectedState(true);
        }

        public void UnselectSpot()
        {
            if (SelectedSpot == null) return;
            
            SelectedSpot.SetSelectedState(false);
            SelectedSpot = null;
        }

        private void OnStateChanged(BattleState newState)
        {
            if (BattleManager.I.StateController.State < BattleState.TurnPlan ||
                BattleManager.I.StateController.State > BattleState.TurnEnd) 
                UnselectSpot();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                UnselectSpot();
            }
        }
    }
}