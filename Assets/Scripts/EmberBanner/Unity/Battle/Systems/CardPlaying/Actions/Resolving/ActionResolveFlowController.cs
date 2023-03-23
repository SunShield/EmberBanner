using EmberBanner.Core.Enums.Battle;
using EmberBanner.Core.Enums.Battle.States;
using EmberBanner.Core.Ingame.Impl.Battles;
using EmberBanner.Unity.Battle.Systems.CardPlaying.TurnPlanning;
using EmberBanner.Unity.Battle.Systems.TurnOrder;
using EmberBanner.Unity.Battle.Systems.Visuals.ActionsResolve;
using EmberBanner.Unity.Battle.Views.Impl.Units.Crystals;

namespace EmberBanner.Unity.Battle.Systems.CardPlaying.Actions.Resolving
{
    /// <summary>
    /// Breakdown of how exactly actions are resolved:
    /// 1. Actions resolve one-after-one, from the topmost to the bottommost.
    /// 2. There are two types of 
    /// </summary>
    public class ActionResolveFlowController
    {
        private static ActionResolveFlowController _instance;
        public static ActionResolveFlowController I => _instance ??= new();
        
        public BattleUnitCrystalView InitiatorCrystal { get; private set; }
        public BattleUnitCrystalView TargetCrystal { get; private set; }
        private BattlePlayingActionEntity _currentAction;
        private BattlePlayingActionEntity _currentTargetAction;
        private bool _isClash;
        private ClashState _clashState;
        
        public ActionsResolveState State { get; private set; }

        public void PickCrystal()
        {
            InitiatorCrystal = TurnOrderController.I.CurrentCrystal;
            
            ActionsResolveUi.I.SetMainCrystal(InitiatorCrystal);
            TargetCrystal = CardTargetsMatrix.I.GetTarget(InitiatorCrystal);
            if (TargetCrystal != InitiatorCrystal)
                ActionsResolveUi.I.SetTargetCrystal(TargetCrystal);
            _isClash = CardTargetsMatrix.I.CheckClash(InitiatorCrystal, TargetCrystal);

            State = ActionsResolveState.GetCurrentActions;
        }

        public void GetCurrentActions()
        {
            if (_currentAction is { IsFullyResolved: true })
            {
                ActionsResolveUi.I.ClearInitiatorMainAction();
                InitiatorCrystal.PickAction(_currentAction);
                _currentAction = null;
            }

            if (_currentTargetAction is { IsFullyResolved: true })
            {
                ActionsResolveUi.I.ClearTargetMainAction();
                TargetCrystal.PickAction(_currentTargetAction);
                _currentTargetAction = null;
            }
            
            ActionsResolveUi.I.UpdateActions();
            
            var actions = CrystalProperActionSelector.I.FindProperActions(InitiatorCrystal, TargetCrystal);
            _currentAction = actions.initiatorAction;
            _currentTargetAction = actions.targetAction;

            if (!_isClash && _currentAction == null ||
                _isClash && _currentAction == null && _currentTargetAction == null)
            {
                State = ActionsResolveState.AllActionsResolved;
                return;
            }

            if (_currentAction != null)
            {
                //InitiatorCrystal.PickAction(_currentAction);
                ActionsResolveUi.I.SetInitiatorMainAction(_currentAction);
            }

            if (_currentTargetAction != null)
            {
                //TargetCrystal.PickAction(_currentTargetAction);
                ActionsResolveUi.I.SetTargetMainAction(_currentTargetAction);
            }
            
            State = ActionsResolveState.RollCurrentActions;
        }

        public void RollCurrentActions()
        {
            _currentAction?.Roll();
            _currentTargetAction?.Roll();
            
            ActionsResolveUi.I.SetRolls(_currentAction?.CurrentRoll, _currentTargetAction?.CurrentRoll);

            if (!_isClash)
            {
                FlipCoins();
            }
            else
            {
                DetermineClashState();
                if (_clashState != ClashState.Tie)
                {
                    FlipCoins();
                }
            }
            ActionsResolveUi.I.UpdateActions();

            State = _clashState == ClashState.Tie 
                ? ActionsResolveState.RollCurrentActions 
                : ActionsResolveState.ResolveCurrentActions;
        }

        public void ResolveCurrentActions()
        {
            if (!_isClash)
            {
                // Resolve actions here
            }
            else
            {
                if (_currentAction != null && _currentTargetAction != null)
                    SetClashLoserMagnitude();

                // Resolve actions here
            }
            
            ActionsResolveUi.I.UpdateActions();
            
            State = ActionsResolveState.PostResolveActions;
        }

        public void PostResolveCurrentActions()
        {
            if (_currentAction       is { IsFullyResolved: false }) _currentAction.PostSingleResolve();
            if (_currentTargetAction is { IsFullyResolved: false }) _currentTargetAction.PostSingleResolve();

            State = ActionsResolveState.GetCurrentActions;
        }

        public void DoOnAllActionsResolved()
        {
            ActionsResolveUi.I.Clear();
            State = ActionsResolveState.FinishResolvingActions;
        }

        private void FlipCoins()
        {
            _currentAction?.FlipCoin();
            _currentTargetAction?.FlipCoin();
        }

        private void DetermineClashState() => _clashState = ClashStateChecker.I.GetClashState(_currentAction, _currentTargetAction);

        private void SetClashLoserMagnitude()
        {
            var loser = GetClashLoserAction(_clashState);
            loser.SetLosingMagnitude();
            ActionsResolveUi.I.SetLosingMagnitude(_clashState);
        }

        private BattlePlayingActionEntity GetClashLoserAction(ClashState state) => state switch
        {
            ClashState.InitiatorWon => _currentTargetAction,
            ClashState.TargetWon    => _currentAction,
        };

        public void ClearAll()
        {
            InitiatorCrystal = null;
            TargetCrystal = null;
            _currentAction = null;
            _currentTargetAction = null;
            _isClash = false;
        }
    }
}