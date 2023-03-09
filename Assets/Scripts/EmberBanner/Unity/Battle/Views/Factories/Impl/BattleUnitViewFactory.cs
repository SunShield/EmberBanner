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
        private static BattleUnitViewFactory _instance;
        public static BattleUnitViewFactory I
        {
            get
            {
                if (_instance == null) _instance = FindObjectOfType<BattleUnitViewFactory>();
                return _instance;
            }
        }
        
        [SerializeField] private BattleUnitView _prefab;

        protected override void PostCreateView(BattleUnitView view)
        {
            var crystals = new List<BattleUnitCrystalView>();
            foreach (var crystalEntity in view.Entity.EnumerateCrystals())
            {
                var crystalEntityTyped = crystalEntity;
                var crystalView = BattleManager.I.CrystalViewFactory.CreateView(crystalEntityTyped);
                crystals.Add(crystalView);
                crystalView.SetOwnerView(view);
            }
            view.SetCrystals(crystals);
            
            BattleManager.I.AddUnit(view);
        }

        protected override BattleUnitView GetPrefab(BattleUnitEntity entity) => _prefab;
    }
}