using EmberBanner.Core.Entities.Impl.Units.Crystals;
using EmberBanner.Core.Ingame.Management.Factories;
using EmberBanner.Core.Models.Units.Crystals;

namespace EmberBanner.Core.Entities.Management.Factories.Impl.Units
{
    public class UnitCrystalEntityFactory : EntityFactory<UnitCrystalEntity, UnitCrystalModel>
    {
        private static UnitCrystalEntityFactory _instance;
        public static UnitCrystalEntityFactory I => _instance ??= new();
    }
}