using EmberBanner.Core.Entities.Management.Factories.Impl.Cards;
using EmberBanner.Core.Ingame.Impl.Battles;
using EmberBanner.Core.Ingame.Management.Factories;
using EmberBanner.Core.Models.Cards;
using EmberBanner.Core.Service.Debug;
using EmberBanner.Unity.Data;

namespace EmberBanner.Core.Entities.Management.Factories.Impl.Battles
{
    public class BattleCardEntityFactory : EntityFactory<BattleCardEntity, CardModel>
    {
        private static BattleCardEntityFactory _instance;
        public static BattleCardEntityFactory I => _instance ??= new();
        
        public BattleCardEntity CreateEntity(string modelName, BattleUnitEntity unitEntity)
        {
            var model = DataHolder.I.Data.Cards[modelName];
            return CreateEntity(model, unitEntity, true);
        }

        protected override void OnPostCreateEntity(BattleCardEntity entity, CardModel model)
        {
            foreach (var actionModel in model.Actions)
            {
                var actionEntity = CardActionEntityFactory.I.CreateEntity(actionModel, null, NextEntityIsTemporary);
                entity.Actions.Add(actionEntity);
            }
            
            var message = $"Card Entity (id: {entity.Id} | model: {model.Name}) created";
            var tempMessage = "Temporary ";
            var finalMessage = NextEntityIsTemporary ? tempMessage : "";
            finalMessage += message;
            
            EBDebugger.Log(EBDebugContext.Entities, EBDebugContext.Cards, finalMessage);
        }
    }
}