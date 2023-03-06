using EmberBanner.Core.Ingame.Units;
using EmberBanner.Core.Models.Units;

namespace EmberBanner.Core.Ingame.Factories
{
    public static class UnitEntityFactory
    {
        public static UnitEntity CreateEntity(UnitModel model)
        {
            var entity = new UnitEntity(EntityIdHolder.GetId, model);
            foreach (var crystalModel in model.Crystals)
            {
                var crystalEntity = UnitCrystalEntityFactory.CreateEntity(crystalModel);
                entity.Crystals.Add(crystalEntity);
            }
            
            return entity;
        }
    }
}