using EmberBanner.Core.Entities.Impl.Cards;
using EmberBanner.Core.Ingame.Management.Factories;
using EmberBanner.Core.Models.Actions;

namespace EmberBanner.Core.Entities.Management.Factories.Impl.Cards
{
    public class CardActionEntityFactory : EntityFactory<CardActionEntity, ActionModel>
    {
        private static CardActionEntityFactory _instance;
        public static CardActionEntityFactory I => _instance ??= new();
    }
}