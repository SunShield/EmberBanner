using EmberBanner.Core.Entities.Impl.Units.Crystals;
using EmberBanner.Core.Models.Units.Crystals;

namespace EmberBanner.Core.Ingame.Management.Cloners.Impl.Units
{
    public class UnitCrystalEntityCloner : EntityCloner<UnitCrystalEntity, UnitCrystalModel>
    {
        private static UnitCrystalEntityCloner _instance;
        public static UnitCrystalEntityCloner I => _instance ??= new();
    }
}