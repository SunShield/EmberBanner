using EmberBanner.Core.Entities.Impl.Cards;
using EmberBanner.Core.Models.Actions;

namespace EmberBanner.Core.Ingame.Management.Cloners.Impl.Cards
{
    public class CardActionEntityCloner : EntityCloner<CardActionEntity, ActionModel>
    {
        private static CardActionEntityCloner _instance;
        public static CardActionEntityCloner I => _instance ??= new();
    }
}