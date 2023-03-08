using System.Collections.Generic;
using EmberBanner.Core.Ingame.Impl.Battles;
using EmberBanner.Core.Models.Units;
using EmberBanner.Unity.Battle.Management;
using EmberBanner.Unity.Battle.Views.Impl.Units;
using EmberBanner.Unity.Battle.Views.Impl.Units.Crystals;
using EmberBanner.Unity.Views;
using UnityEngine;

namespace EmberBanner.Unity.Battle.Views.Factories.Impl
{
    public class BattleUnitViewFactory : ViewFactory<BattleUnitView, BattleUnitEntity, UnitModel>
    {
        [SerializeField] private BattleUnitView _prefab;

        protected override void PostCreateView(BattleUnitView view)
        {
            var crystals = new List<BattleUnitCrystalView>();
            foreach (var crystalEntity in view.Entity.Crystals)
            {
                var crystalEntityTyped = crystalEntity as BattleUnitCrystalEntity;
                var crystalView = BattleManager.I.CrystalViewFactory.CreateView(crystalEntityTyped);
                crystals.Add(crystalView);
            }
            view.UnitCrystals.SetCrystals(crystals);
        }

        protected override BattleUnitView GetPrefab(BattleUnitEntity entity) => _prefab;
    }
}