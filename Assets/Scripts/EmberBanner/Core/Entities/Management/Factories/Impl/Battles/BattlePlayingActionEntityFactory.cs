using EmberBanner.Core.Entities.Impl.Cards;
using EmberBanner.Core.Ingame.Impl.Battles;

namespace EmberBanner.Core.Entities.Management.Factories.Impl.Battles
{
    public class BattlePlayingActionEntityFactory
    {
        private static BattleCardEntityFactory _instance;
        public static BattleCardEntityFactory I => _instance ??= new();

        public BattlePlayingActionEntity CreateEntity(CardActionEntity baseEntity) =>
            new (baseEntity);
    }
}