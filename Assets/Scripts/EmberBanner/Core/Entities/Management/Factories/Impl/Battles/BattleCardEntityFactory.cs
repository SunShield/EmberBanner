using EmberBanner.Core.Enums.Battle;
using EmberBanner.Core.Ingame.Impl.Battles;
using EmberBanner.Core.Ingame.Management.Factories;
using EmberBanner.Core.Models.Cards;
using EmberBanner.Unity.Data;

namespace EmberBanner.Core.Entities.Management.Factories.Impl.Battles
{
    public class BattleCardEntityFactory : EntityFactory<BattleCardEntity, CardModel>
    {
        private static BattleCardEntityFactory _instance;
        public static BattleCardEntityFactory I => _instance ??= new();
        
        public BattleCardEntity CreateEntity(string modelName)
        {
            var model = DataHolder.I.Data.Cards[modelName];
            return CreateEntity(model, BattleCardZone.None, true);
        }
    }
}