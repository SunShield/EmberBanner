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

        public UnitEntity CreateEntity(string modelName, object payload, bool temporaryEntity = false)
        {
            var model = DataHolder.I.Databases.Units[modelName];
            return CreateEntity(model, null, temporaryEntity);
        }

        protected override void OnPostCreateEntity(UnitEntity entity, UnitModel model)
        {
            foreach (var crystalModel in model.Crystals)
            {
                var crystalEntity = UnitCrystalEntityFactory.I.CreateEntity(crystalModel, entity, NextEntityIsTemporary);
                entity.AddCrystal(crystalEntity);
            }
            
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