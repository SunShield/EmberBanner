using EmberBanner.Core.Entities.Impl.Cards;
using EmberBanner.Core.Ingame.Management.Factories;
using EmberBanner.Core.Models.Actions;
using EmberBanner.Core.Service.Debug;

namespace EmberBanner.Core.Entities.Management.Factories.Impl.Cards
{
    public class CardActionEntityFactory : EntityFactory<CardActionEntity, ActionModel>
    {
        private static CardActionEntityFactory _instance;
        public static CardActionEntityFactory I => _instance ??= new();

        protected override void OnPostCreateEntity(CardActionEntity entity, ActionModel model)
        {
            var message = $"Card Action Entity (id: {entity.Id} | type: {model.Type} | magnitude: {entity.Magnitude.CalculateValue()}) created";
            var tempMessage = "Temporary ";
            var finalMessage = NextEntityIsTemporary ? tempMessage : "";
            finalMessage += message;
            
            EBDebugger.Log(EBDebugContext.Entities, EBDebugContext.Cards, finalMessage);
        }
    }
}