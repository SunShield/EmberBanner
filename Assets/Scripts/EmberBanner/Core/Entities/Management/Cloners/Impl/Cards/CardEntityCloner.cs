using EmberBanner.Core.Entities.Impl.Cards;
using EmberBanner.Core.Entities.Management.SaveLoad.Data.Impl.Cards;
using EmberBanner.Core.Models.Cards;

namespace EmberBanner.Core.Ingame.Management.Cloners.Impl.Cards
{
    public class CardEntityCloner : SavableEntityCloner<CardEntity, CardModel, CardSaveData>
    {
        private static CardEntityCloner _instance;
        public static CardEntityCloner I => _instance ??= new();
        
        protected override void ProcessEntityPostInitialize(CardEntity originalEntity, CardEntity entityClone)
        {
            // cloning card actions
            foreach (var originalEntityAction in originalEntity.Actions)
            {
                var actionClone = CardActionEntityCloner.I.Clone(originalEntityAction);
                entityClone.Actions.Add(actionClone);
            }
        }
    }
}