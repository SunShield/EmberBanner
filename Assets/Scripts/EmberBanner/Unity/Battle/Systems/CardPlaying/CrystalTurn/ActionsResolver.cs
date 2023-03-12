using System.Collections.Generic;
using EmberBanner.Core.Ingame.Impl.Battles;
using EmberBanner.Unity.Battle.Systems.TurnOrder;
using EmberBanner.Unity.Battle.Views.Impl.Units.Crystals;

namespace EmberBanner.Unity.Battle.Systems.CardPlaying.CrystalTurn
{
    /// <summary>
    /// Breakdown of how exactly actions are resolved:
    /// 1. Actions resolve one-after-one, from the topmost to the bottommost.
    /// 2. There are two types of 
    /// </summary>
    public class ActionsResolver
    {
        private static ActionsResolver _instance;
        public static ActionsResolver I => _instance ??= new();
        
        public BattleUnitCrystalView CurrentCrystal { get; private set; }
        public List<BattlePlayingActionEntity> CurrentActions { get; private set; } = new();
        private bool CrystalHasAction => CurrentActions.Count > 0;
        private BattlePlayingActionEntity _currentAction;

        /// <summary>
        /// Empty for now
        ///
        /// Fast actions are resolved here
        /// </summary>
        public void CrystalsPreTurn()
        {
            GetCurrentCrystal();
        }

        private void GetCurrentCrystal()
        {
            CurrentCrystal = TurnOrderController.I.CurrentCrystal;
            CurrentActions = CurrentCrystal.Actions;
        }

        private void ProcessNextAction()
        {
            _currentAction = CurrentActions[0];
        }
    }
}