using System.Collections.Generic;
using EmberBanner.Core.Enums.Battle;
using EmberBanner.Core.Ingame.Impl.Battles;
using EmberBanner.Core.Models.Units;
using EmberBanner.Unity.Battle.Systems.CardZonesSystem;
using EmberBanner.Unity.Battle.Views.Impl.Units.Crystals;
using UnityEngine;

namespace EmberBanner.Unity.Battle.Views.Impl.Units
{
    public class BattleUnitView : BattleView<BattleUnitEntity, UnitModel>
    {
        [SerializeField] private SpriteRenderer _graphics;
        [SerializeField] private BattleUnitCrystalsView _unitCrystals;
        private UnitCardZonesManager _zonesManager;

        public UnitControllerType Controller => Entity.Controller;
        public BattleUnitCrystalsView UnitCrystals => _unitCrystals;

        public void SetCrystals(List<BattleUnitCrystalView> crystals) => _unitCrystals.SetCrystals(crystals);
    }
}