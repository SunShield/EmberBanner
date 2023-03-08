using EmberBanner.Core.Entities.Impl.Units;
using EmberBanner.Core.Entities.Management.Databases;
using EmberBanner.Core.Ingame.Management.Factories;
using EmberBanner.Core.Models.Units;
using EmberBanner.Core.Service.Debug;
using EmberBanner.Unity.Data;

namespace EmberBanner.Core.Entities.Management.Factories.Impl.Units
{
    public class UnitEntityFactory : EntityFactory<UnitEntity, UnitModel>
    {
        private static UnitEntityFactory _instance;
        public static UnitEntityFactory I => _instance ??= new();

        public UnitEntity CreateEntity(string modelName, bool temporaryEntity = false)
        {
            var model = DataHolder.I.Data.Units[modelName];
            return CreateEntity(model, temporaryEntity);
        }

        protected override void OnPostCreateEntity(UnitEntity entity, UnitModel model)
        {
            var message = $"Unit Entity (id: {entity.Id} | model: {model.Name}) created";
            var tempMessage = "Temporary ";
            var finalMessage = NextEntityIsTemporary ? tempMessage : "";
            finalMessage += message;
            
            EBDebugger.Log(EBDebugContext.Entities, EBDebugContext.Units, finalMessage);
            
            if (!NextEntityIsTemporary)
                GeneralEntityDatabase.I.Units.AddEntity(entity);
        }
    }
}