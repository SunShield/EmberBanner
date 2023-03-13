using System.Collections.Generic;
using EmberBanner.Core.Enums.Battle.States;
using EmberBanner.Core.Ingame.Impl.Battles;
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
        public List<BattlePlayingActionEntity> CurrentActions { get; private set; } = new();
        private bool CrystalHasAction => CurrentActions.Count > 0;
        private BattlePlayingActionEntity _currentAction;
        
        public ActionsResolveState State { get; private set; }

        public void PickCrystal()
        {
            InitiatorCrystal = TurnOrderController.I.CurrentCrystal;
            CurrentActions = InitiatorCrystal.Actions;
            ActionsResolveUi.I.SetMainCrystal(InitiatorCrystal);

            State = ActionsResolveState.GetCurrentAction;
        }

        public void GetCurrentAction()
        {
            _currentAction = InitiatorCrystal.GetFirstNonCancelledAction();
            ActionsResolveUi.I.SetCurrentMainAction(_currentAction);
            State = ActionsResolveState.GetOpposedAction;
        }

        public void GetOpposingAction()
        {
            var target = _currentAction.Target;
            if (target != InitiatorCrystal) 
                ActionsResolveUi.I.SetTargetCrystal(target);
            else
                ActionsResolveUi.I.HideTargetUi(); // If we are targeting self, no need in target uis
            State = ActionsResolveState.RollCurrentActions;
        }

        public void RollCurrentActions()
        {
            State = ActionsResolveState.ResolveCurrentActions;
        }

        public void ResolveCurrentActions()
        {
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
    }
}