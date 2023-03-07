using EmberBanner.Core.Entities.Impl.Units;
using EmberBanner.Core.Entities.Management.SaveLoad.Data.Impl.Units;
using EmberBanner.Core.Models.Units;
using EmberBanner.Unity.Data;

namespace EmberBanner.Core.Entities.Management.Databases.Impl
{
    public class UnitEntitiesDatabase : EntityDatabase<UnitEntity, UnitModel, UnitSaveData>
    {
        protected override UnitModel GetModel(string name) => DataHolder.I.Data.Units.Elements[name];
    }
}