using EmberBanner.Core.Enums.Battle;
using EmberBanner.Core.Ingame.Impl.Battles;
using EmberBanner.Core.Ingame.Management.Factories;
using EmberBanner.Core.Models.Units;
using EmberBanner.Core.Service.Debug;
using EmberBanner.Unity.Data;

namespace EmberBanner.Core.Entities.Management.Factories.Impl.Battles
{
    public class BattleUnitEntityFactory : EntityFactory<BattleUnitEntity, UnitModel>
    {
        private static BattleUnitEntityFactory _instance;
        public static BattleUnitEntityFactory I => _instance ??= new();

        public BattleUnitEntity CreateEntity(string modelName, UnitControllerType controller)
        {
            var model = DataHolder.I.Data.Units[modelName];
            return CreateEntity(model, controller, true);
        }

        protected override void OnPostCreateEntity(BattleUnitEntity entity, UnitModel model)
        {
            foreach (var crystalModel in model.Crystals)
            {
                var crystalEntity = BattleUnitCrystalEntityFactory.I.CreateEntity(crystalModel, entity, true);
                entity.AddCrystal(crystalEntity);
            }

            var controllerString = entity.Controller.ToString();
            var message = $"Battle Unit Entity (controller: {controllerString} | id: {entity.Id} | model: {model.Name}) created";
            
            EBDebugger.Log(EBDebugContext.Battle, EBDebugContext.Entities, EBDebugContext.Units, message);
        }
    }
}