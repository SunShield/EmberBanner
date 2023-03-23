using System;
using EmberBanner.Core.Enums.Battle.States;
using EmberBanner.Unity.Battle.Management;
using EmberBanner.Unity.Battle.Systems.CardPlaying.Actions;
using EmberBanner.Unity.Battle.Systems.CardPlaying.Actions.Resolving;
using EmberBanner.Unity.Battle.Systems.CardPlaying.PostTurnPlanning;
using EmberBanner.Unity.Battle.Systems.CardPlaying.TurnPlanning;
using EmberBanner.Unity.Battle.Systems.EnemyAttacks;
using EmberBanner.Unity.Battle.Systems.Selection;
using EmberBanner.Unity.Battle.Systems.Startup;
using EmberBanner.Unity.Battle.Systems.TurnOrder;
using EmberBanner.Unity.Battle.Systems.Visuals.Arrows;
using EmberBanner.Unity.Battle.Systems.Visuals.PrePlayedCards;
using EmberBanner.Unity.Battle.Views.Impl.Units;
using EmberBanner.Unity.Service;
using TMPro;
using UnityEngine;

namespace EmberBanner.Unity.Battle.Systems.StateSystem
{
    public class BattleStateController : EBMonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _stateText;
        [SerializeField] private TextMeshProUGUI _resolveState;
        public BattleState State { get; private set; } = BattleState.PreStart;
        public int TurnNumber { get; private set; } = 0;
        private bool IsFirstTurn => TurnNumber == 1;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                ProceedState();
            }
        }
        
        public void ProceedState()
        {
            if (State == BattleState.PreStart)
            {
                BattleStarter.I.StartBattle();
                State = BattleState.Start;
            }
            else if (State == BattleState.Start)
            {
                State = BattleState.TurnStart;
            }
            else if (State == BattleState.TurnStart)
            {
                AdvanceTurn();
                OnTurnStart();
                State = BattleState.TurnPrePlan;
            }
            else if (State == BattleState.TurnPrePlan)
            {
                OnTurnPrePlan();
                TurnOrderController.I.DetermineTurnOrder();
                EnemyAttackPlanner.I.SetEnemyAttacks();
                State = BattleState.TurnPlan;
            }
            else if (State == BattleState.TurnPlan)
            {
                State = BattleState.PostTurnPlan;
            }
            else if (State == BattleState.PostTurnPlan)
            {
                CardSelectionManager.I.UnselectCard();
                CrystalSelectionManager.I.UnselectCrystal();
                UnitSelectionManager.I.UnselectSpot();
                
                PrePlayedCardsUiManager.I.Clear();
                TurnOrderController.I.ClearNonActingCrystals();
                CrystalActionsFiller.I.AddActionsToCrystals();
                State = BattleState.CrystalTurnStart;
            }
            else if (State == BattleState.CrystalTurnStart)
            {
                while (true)
                {
                    if (TurnOrderController.I.AllCrystalsEndedTurns)
                    {
                        ActionResolveFlowController.I.DoOnAllActionsResolved();
                        State = BattleState.TurnEnd;
                        return;
                    }
                    
                    if (!TurnOrderController.I.CurrentCrystal.HasNonCancelledActions)
                        TurnOrderController.I.AdvanceOrder();
                    else
                        break;
                }
                
                ActionResolveFlowController.I.PickCrystal();
                State = BattleState.CrustalTurn;
            }
            else if (State == BattleState.CrustalTurn)
            {
                if (ActionResolveFlowController.I.State == ActionsResolveState.GetCurrentActions)
                {
                    ActionResolveFlowController.I.GetCurrentActions();
                    if (ActionResolveFlowController.I.State == ActionsResolveState.AllActionsResolved)
                        State = BattleState.CrystalTurnEnd;
                }
                else if (ActionResolveFlowController.I.State == ActionsResolveState.RollCurrentActions)
                {
                    ActionResolveFlowController.I.RollCurrentActions();
                }  
                else if (ActionResolveFlowController.I.State == ActionsResolveState.ResolveCurrentActions)
                {
                    ActionResolveFlowController.I.ResolveCurrentActions();
                } 
                else if (ActionResolveFlowController.I.State == ActionsResolveState.PostResolveActions)
                {
                    ActionResolveFlowController.I.PostResolveCurrentActions();
                    if (ActionResolveFlowController.I.State == ActionsResolveState.AllActionsResolved)
                        State = BattleState.CrystalTurnEnd;
                } 
            }
            else if (State == BattleState.CrystalTurnEnd)
            {
                if (ActionResolveFlowController.I.State == ActionsResolveState.AllActionsResolved)
                {
                    ActionResolveFlowController.I.DoOnAllActionsResolved();
                } 
                else if (ActionResolveFlowController.I.State == ActionsResolveState.FinishResolvingActions)
                {
                    if (TurnOrderController.I.AllCrystalsEndedTurns)
                        State = BattleState.TurnEnd;
                    else
                    {
                        TurnOrderController.I.AdvanceOrder();
                        State = BattleState.CrystalTurnStart;
                    }
                } 
            }
            else if (State == BattleState.TurnEnd)
            {
                OnTurnEnd();
                State = BattleState.TurnStart;
            }
            else if (State == BattleState.PreEnd)
            {
                State = BattleState.End;
            }

            onStateChanged?.Invoke(State);
            _stateText.text = State.ToString();
            _resolveState.text = ActionResolveFlowController.I.State.ToString();
        }

        private void AdvanceTurn() => TurnNumber++;

        private void OnTurnStart()
        {
            foreach (var unit in BattleManager.I.Registry.Units.Values)
            {
                BattleUnitView.BattleUnitStateHandler.OnTurnStart(unit, IsFirstTurn);
            }
        }

        private void OnTurnPrePlan()
        {
            foreach (var unit in BattleManager.I.Registry.Units.Values)
            {
                BattleUnitView.BattleUnitStateHandler.OnTurnPrePlan(unit);
            }
        }

        private void OnTurnEnd()
        {
            CardTargetsMatrix.I.Clear();
            ActionResolveFlowController.I.ClearAll();
            CardTargetsMatrixUi.I.OnTurnEnd();
            
            foreach (var unit in BattleManager.I.Registry.Units.Values)
            {
                BattleUnitView.BattleUnitStateHandler.OnTurnEnd(unit);
            }
        }

        public event Action<BattleState> onStateChanged;
    }
}