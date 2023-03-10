using EmberBanner.Core.Ingame.Impl.Battles;
using EmberBanner.Core.Models.Units.Crystals;
using EmberBanner.Unity.Battle.Views.Impl.Units.Crystals;
using EmberBanner.Unity.Views;
using UnityEngine;

namespace EmberBanner.Unity.Battle.Views.Factories.Impl
{
    public class BattleUnitCrystalViewFactory : ViewFactory<Views.Impl.Units.Crystals.BattleUnitCrystalView, BattleUnitCrystalEntity, UnitCrystalModel>
    {
        [SerializeField] private Views.Impl.Units.Crystals.BattleUnitCrystalView _prefab;

        protected override Views.Impl.Units.Crystals.BattleUnitCrystalView GetPrefab(BattleUnitCrystalEntity entity) => _prefab;
    }
}