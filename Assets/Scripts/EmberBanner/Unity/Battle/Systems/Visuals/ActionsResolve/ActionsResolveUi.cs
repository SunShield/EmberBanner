using System;
using System.Collections.Generic;
using EmberBanner.Core.Enums.Battle;
using EmberBanner.Core.Ingame.Impl.Battles;
using EmberBanner.Core.Service.Extensions;
using EmberBanner.Unity.Battle.Views.Impl.Units.Crystals;
using EmberBanner.Unity.Service;
using UnityEngine;

namespace EmberBanner.Unity.Battle.Systems.Visuals.ActionsResolve
{
    public class ActionsResolveUi : EBMonoBehaviour
    {
        private static ActionsResolveUi _instance;
        public static ActionsResolveUi I
        {
            get
            {
                if (_instance == null)
                    _instance = FindObjectOfType<ActionsResolveUi>(true);
                return _instance;
            }
        }
        
        [SerializeField] private ResolvingUnitUi _playerUi;
        [SerializeField] private ResolvingUnitUi _enemyUi;
        [SerializeField] private ResolvingActionsUi _playerActions;
        [SerializeField] private ResolvingActionsUi _enemyActions;
        [SerializeField] private CurrentActionUi _playerCurrentAction;
        [SerializeField] private CurrentActionUi _enemyCurrentAction;
        
        private Dictionary<UnitControllerType, (ResolvingUnitUi unitUi, ResolvingActionsUi actionsUi, CurrentActionUi currentActionUi)> _uiMap;
        private BattleUnitCrystalView _initiatorCrystal;
        private BattleUnitCrystalView _targetCrystal;

        private UnitControllerType InitiatorCrystalController => _initiatorCrystal.Controller;
        private UnitControllerType TargetCrystalController => _targetCrystal.Controller; 
        private (ResolvingUnitUi unitUi, ResolvingActionsUi actionsUi, CurrentActionUi currentActionUi) InitiatorCrystalUis => _uiMap[InitiatorCrystalController];
        private (ResolvingUnitUi unitUi, ResolvingActionsUi actionsUi, CurrentActionUi currentActionUi) TargetCrystalUis => _uiMap[TargetCrystalController];
        
        private void Awake()
        {
            _uiMap = new()
            {
                { UnitControllerType.Player, (_playerUi, _playerActions, _playerCurrentAction) },
                { UnitControllerType.Enemy,  (_enemyUi,  _enemyActions,  _enemyCurrentAction) },
            };
        }
        
        public void SetMainCrystal(BattleUnitCrystalView mainCrystal)
        {
            gameObject.SetActive(true);
            _initiatorCrystal = mainCrystal;
            
            InitiatorCrystalUis.unitUi.gameObject.SetActive(true);
            InitiatorCrystalUis.actionsUi.gameObject.SetActive(true);
            InitiatorCrystalUis.unitUi.SetUnit(_initiatorCrystal.OwnerView);
            InitiatorCrystalUis.actionsUi.SetActions(_initiatorCrystal);
        }

        public void SetCurrentMainAction(BattlePlayingActionEntity action)
        {
            InitiatorCrystalUis.currentActionUi.gameObject.SetActive(true);
            InitiatorCrystalUis.currentActionUi.SetAction(action);
            InitiatorCrystalUis.actionsUi.SetActions(_initiatorCrystal);
        }

        public void SetTargetCrystal(BattleUnitCrystalView secondaryCrystal)
        {
            _targetCrystal = secondaryCrystal;
            TargetCrystalUis.unitUi.gameObject.SetActive(true);
            TargetCrystalUis.actionsUi.gameObject.SetActive(true);
            TargetCrystalUis.unitUi.SetUnit(_targetCrystal.OwnerView);
            TargetCrystalUis.actionsUi.SetActions(_targetCrystal);
        }

        public void HideTargetUi()
        {
            TargetCrystalUis.unitUi.gameObject.SetActive(false);
            TargetCrystalUis.actionsUi.gameObject.SetActive(false);
            TargetCrystalUis.currentActionUi.gameObject.SetActive(false);
        }
    }
}