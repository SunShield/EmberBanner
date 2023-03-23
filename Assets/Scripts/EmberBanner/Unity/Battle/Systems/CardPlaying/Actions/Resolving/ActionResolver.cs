using EmberBanner.Core.Ingame.Impl.Battles;

namespace EmberBanner.Unity.Battle.Systems.CardPlaying.Actions.Resolving
{
    public class ActionResolver
    {
        private static ActionResolver _instance;
        public static ActionResolver I => _instance ??= new();

        public void ResolveActionPair(BattlePlayingActionEntity initiatorAction, BattlePlayingActionEntity targetAction, bool isClash)
        {
            
        }
    }
}