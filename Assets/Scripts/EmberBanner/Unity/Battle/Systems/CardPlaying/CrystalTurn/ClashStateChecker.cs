using EmberBanner.Core.Enums.Battle;
using EmberBanner.Core.Ingame.Impl.Battles;

namespace EmberBanner.Unity.Battle.Systems.CardPlaying.CrystalTurn
{
    public class ClashStateChecker
    {
        private static ClashStateChecker _instance;
        public static ClashStateChecker I => _instance ??= new();

        public ClashState GetClashState(BattlePlayingActionEntity initiatorAction,
            BattlePlayingActionEntity targetAction)
        {
            if (initiatorAction != null && targetAction == null) return ClashState.InitiatorWon;
            if (targetAction != null && initiatorAction == null) return ClashState.TargetWon;
            
            if (initiatorAction.CurrentRoll.Value > targetAction.CurrentRoll.Value) return ClashState.InitiatorWon;
            if (initiatorAction.CurrentRoll.Value < targetAction.CurrentRoll.Value) return ClashState.TargetWon;
            return ClashState.Tie;
        }
    }
}