using System.Collections.Generic;
using EmberBanner.Core.Enums.Battle;
using EmberBanner.Core.Ingame.Units;
using EmberBanner.Unity.Battle.Management;
using EmberBanner.Unity.Battle.Views.Units;
using EmberBanner.Unity.Battle.Views.Units.Crystals;
using EmberBanner.Unity.Service;
using UnityEngine;

namespace EmberBanner.Unity.Battle.Views.Factories
{
    public class BattleUnitViewFactory : EBMonoBehaviour
    {
        [SerializeField] private BattleUnitView _prefab;

        public BattleUnitView SpawnUnit(UnitEntity entity, UnitControllerType controller)
        {
            var unit = Instantiate(_prefab);
            unit.Initialize(entity, controller);
            var crystals = SpawnCrystals(unit);
            unit.UnitCrystals.SetCrystals(crystals);
            
            return unit;
        }

        private List<BattleUnitCrystalView> SpawnCrystals(BattleUnitView unit)
        {
            var crystals = new List<BattleUnitCrystalView>();
            foreach (var crystalEntity in unit.Entity.Crystals)
            {
                var crystalView = BattleManager.I.CrystalViewFactory.SpawnCrystal(crystalEntity, unit);
                crystals.Add(crystalView);
            }

            return crystals;
        }
    }
}