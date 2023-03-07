using EmberBanner.Core.Entities.Impl.Cards;
using EmberBanner.Core.Entities.Management.Databases;
using EmberBanner.Core.Ingame.Management.Factories;
using EmberBanner.Core.Models.Cards;
using EmberBanner.Unity.Data;

namespace EmberBanner.Core.Entities.Management.Factories.Impl.Cards
{
    public class CardEntityFactory : EntityFactory<CardEntity, CardModel>
    {
        private static CardEntityFactory _instance;
        public static CardEntityFactory I => _instance ??= new();
        
        public CardEntity CreateEntity(string modelName)
        {
            var model = DataHolder.I.Data.Cards[modelName];
            return CreateEntity(model);
        }

        protected override void OnPostCreateEntity(CardEntity entity, CardModel model)
        {
            GeneralEntityDatabase.I.Cards.AddEntity(entity);
        }
    }
}