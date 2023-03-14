using System;
using EmberBanner.Core.Enums.Battle;
using EmberBanner.Core.Enums.Battle.States;
using EmberBanner.Core.Ingame.Impl.Battles;
using EmberBanner.Unity.Battle.Systems.CardPlaying.TurnPlanning;
using EmberBanner.Unity.Battle.Systems.TurnOrder;
using EmberBanner.Unity.Battle.Systems.Visuals.ActionsResolve;
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
        
        public BattleUnitCrystalView InitiatorCrystal { get; private set; }
        public BattleUnitCrystalView TargetCrystal { get; private set; }
        private BattlePlayingActionEntity _currentAction;
        private BattlePlayingActionEntity _currentTargetAction;
        
        public ActionsResolveState State { get; private set; }

        public void PickCrystal()
        {
            InitiatorCrystal = TurnOrderController.I.CurrentCrystal;
            ActionsResolveUi.I.SetMainCrystal(InitiatorCrystal);
            TargetCrystal = CardTargetsMatrix.I.GetTarget(InitiatorCrystal);
            if (TargetCrystal != InitiatorCrystal)
                ActionsResolveUi.I.SetTargetCrystal(TargetCrystal);

            State = ActionsResolveState.GetCurrentActions;
        }

        public void GetCurrentActions()
        {
            var actions = CrystalProperActionSelector.I.FindProperActions(InitiatorCrystal, TargetCrystal);
            _currentAction = actions.initiatorAction;
            _currentTargetAction = actions.targetAction;

            if (_currentAction != null)
            {
                InitiatorCrystal.PickAction(_currentAction);
                ActionsResolveUi.I.SetInitiatorMainAction(_currentAction);
            }

            if (_currentTargetAction != null)
            {
                TargetCrystal.PickAction(_currentTargetAction);
                ActionsResolveUi.I.SetTargetMainAction(_currentTargetAction);
            }
            
            State = ActionsResolveState.RollCurrentActions;
        }

        public void RollCurrentActions()
        {
            _currentAction.Roll();
            _currentTargetAction.Roll();
            ActionsResolveUi.I.SetRolls(_currentAction.CurrentRoll.Value, _currentTargetAction.CurrentRoll.Value);
            State = ActionsResolveState.ResolveCurrentActions;
        }

        public void ResolveCurrentActions()
        {
            var clashState = ClashStateChecker.I.GetClashState(_currentAction, _currentTargetAction);
            if (clashState == ClashState.Tie)
            {
                State = ActionsResolveState.RollCurrentActions;
                return;
            }

            var loser = GetClashLoserAction(clashState);
            var losingMagnitude = loser.GetLosingMagnitude();
            ActionsResolveUi.I.SetLosingMagnitude(losingMagnitude, clashState);

            loser.FlipCoin();
            ActionsResolveUi.I.UpdateActions();
            
            State = ActionsResolveState.PostResolveActions;
        }

        public void PostResolveCurrentActions()
        {
            State = ActionsResolveState.AllActionsResolved;
        }

        public void DoOnAllActionsResolved()
        {
            State = ActionsResolveState.FinishResolvingActions;
        }

        private BattlePlayingActionEntity GetClashLoserAction(ClashState state) => state switch
        {
            ClashState.InitiatorWon => _currentTargetAction,
            ClashState.TargetWon    => _currentAction,
        };
    }
}