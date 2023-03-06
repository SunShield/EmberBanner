using EmberBanner.Core.Ingame.Units.Crystals;
using EmberBanner.Core.Models.Units.Crystals;

namespace EmberBanner.Core.Ingame.Factories
{
    public static class UnitCrystalEntityFactory
    {
        public static UnitCrystalEntity CreateEntity(UnitCrystalModel model)
        {
            var entity = new UnitCrystalEntity(EntityIdHolder.GetId, model);
            return entity;
        }
    }
}