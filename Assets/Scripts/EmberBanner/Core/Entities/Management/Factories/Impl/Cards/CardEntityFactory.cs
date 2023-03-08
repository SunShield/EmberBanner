using EmberBanner.Core.Entities.Impl.Cards;
using EmberBanner.Core.Entities.Management.Databases;
using EmberBanner.Core.Ingame.Management.Factories;
using EmberBanner.Core.Models.Cards;
using EmberBanner.Core.Service.Debug;
using EmberBanner.Unity.Data;

namespace EmberBanner.Core.Entities.Management.Factories.Impl.Cards
{
    public class CardEntityFactory : EntityFactory<CardEntity, CardModel>
    {
        private static CardEntityFactory _instance;
        public static CardEntityFactory I => _instance ??= new();
        
        public CardEntity CreateEntity(string modelName, bool temporaryEntity = false)
        {
            var model = DataHolder.I.Data.Cards[modelName];
            return CreateEntity(model, temporaryEntity);
        }

        protected override void OnPostCreateEntity(CardEntity entity, CardModel model)
        {
            var message = $"Card Entity (id: {entity.Id} | model: {model.Name}) created";
            var tempMessage = "Temporary ";
            var finalMessage = NextEntityIsTemporary ? tempMessage : "";
            finalMessage += message;
            
            EBDebugger.Log(EBDebugContext.Entities, EBDebugContext.Cards, finalMessage);
            
            if (!NextEntityIsTemporary)
                GeneralEntityDatabase.I.Cards.AddEntity(entity);
        }
    }
}