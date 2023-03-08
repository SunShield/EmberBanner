using EmberBanner.Core.Entities.Impl.Units.Crystals;
using EmberBanner.Unity.Battle.Views.Impl.Units;
using EmberBanner.Unity.Battle.Views.Impl.Units.Crystals;
using EmberBanner.Unity.Service;
using UnityEngine;

namespace EmberBanner.Unity.Battle.Views.Factories
{
    public class BattleUnitCrystalViewFactory : EBMonoBehaviour
    {
        [SerializeField] private BattleUnitCrystalView _prefab;

        public BattleUnitCrystalView SpawnCrystal(UnitCrystalEntity entity, BattleUnitView owner)
        {
            var instance = Instantiate(_prefab);
            instance.Initialize(entity, owner);
            return instance;
        }
    }
}