using EmberBanner.Core.Entities.Impl.Units;
using EmberBanner.Core.Entities.Management.Databases;
using EmberBanner.Core.Ingame.Management.Factories;
using EmberBanner.Core.Models.Units;
using EmberBanner.Unity.Data;

namespace EmberBanner.Core.Entities.Management.Factories.Impl.Units
{
    public class UnitEntityFactory : EntityFactory<UnitEntity, UnitModel>
    {
        private static UnitEntityFactory _instance;
        public static UnitEntityFactory I => _instance ??= new();

        public UnitEntity CreateEntity(string modelName)
        {
            var model = DataHolder.I.Data.Units[modelName];
            return CreateEntity(model);
        }

        protected override void OnPostCreateEntity(UnitEntity entity, UnitModel model)
        {
            GeneralEntityDatabase.I.Units.AddEntity(entity);
        }
    }
}