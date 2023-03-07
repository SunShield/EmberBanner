using EmberBanner.Core.Entities.Impl.Units;
using EmberBanner.Core.Enums.Battle;
using EmberBanner.Core.Models.Units;
using EmberBanner.Unity.Battle.Systems.CardZonesSystem;
using UnityEngine;

namespace EmberBanner.Unity.Battle.Views.Units
{
    public class BattleUnitView : BattleView<UnitModel, UnitEntity>
    {
        [SerializeField] private BattleUnitCrystalsView _unitCrystals;
        private UnitCardZonesManager _zonesManager;
        
        public UnitControllerType Controller { get; private set; }

        public BattleUnitCrystalsView UnitCrystals => _unitCrystals;

        public void Initialize(UnitEntity entity, UnitControllerType controller)
        {
            Entity = entity;
            Controller = controller;
        }
    }
}