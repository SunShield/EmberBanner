using System;
using EmberBanner.Core.Enums.Battle.States;
using EmberBanner.Unity.Battle.Management;
using EmberBanner.Unity.Battle.Systems.CardPlaying.CrystalTurn;
using EmberBanner.Unity.Battle.Systems.CardPlaying.PostTurnPlanning;
using EmberBanner.Unity.Battle.Systems.CardPlaying.TurnPlanning;
using EmberBanner.Unity.Battle.Systems.EnemyAttacks;
using EmberBanner.Unity.Battle.Systems.Startup;
using EmberBanner.Unity.Battle.Systems.TurnOrder;
using EmberBanner.Unity.Battle.Systems.Visuals.Arrows;
using EmberBanner.Unity.Battle.Systems.Visuals.PrePlayedCards;
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
                TurnNumber++;
                DrawCards();
                RollCrystals();
                EnemyAttackPlanner.I.SetEnemyAttacks();
                TurnOrderController.I.DetermineTurnOrder();
                State = BattleState.TurnPlan;
            }
            else if (State == BattleState.TurnPlan)
            {
                State = BattleState.PostTurnPlan;
            }
            else if (State == BattleState.PostTurnPlan)
            {
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
                        ActionsResolver.I.DoOnAllActionsResolved();
                        State = BattleState.TurnEnd;
                        return;
                    }
                    
                    if (!TurnOrderController.I.CurrentCrystal.HasNonCancelledActions)
                    {
                        TurnOrderController.I.AdvanceOrder();
                    }
                    else
                    {
                        break;
                    }
                }
                
                ActionsResolver.I.PickCrystal();
                State = BattleState.CrustalTurn;
            }
            else if (State == BattleState.CrustalTurn)
            {
                if (ActionsResolver.I.State == ActionsResolveState.GetCurrentActions)
                {
                    ActionsResolver.I.GetCurrentActions();
                    if (ActionsResolver.I.State == ActionsResolveState.AllActionsResolved)
                        State = BattleState.CrystalTurnEnd;
                }
                else if (ActionsResolver.I.State == ActionsResolveState.RollCurrentActions)
                {
                    ActionsResolver.I.RollCurrentActions();
                }  
                else if (ActionsResolver.I.State == ActionsResolveState.ResolveCurrentActions)
                {
                    ActionsResolver.I.ResolveCurrentActions();
                } 
                else if (ActionsResolver.I.State == ActionsResolveState.PostResolveActions)
                {
                    ActionsResolver.I.PostResolveCurrentActions();
                    if (ActionsResolver.I.State == ActionsResolveState.AllActionsResolved)
                        State = BattleState.CrystalTurnEnd;
                } 
            }
            else if (State == BattleState.CrystalTurnEnd)
            {
                if (ActionsResolver.I.State == ActionsResolveState.AllActionsResolved)
                {
                    ActionsResolver.I.DoOnAllActionsResolved();
                } 
                else if (ActionsResolver.I.State == ActionsResolveState.FinishResolvingActions)
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
                ClearTurn();
                State = BattleState.TurnStart;
            }
            else if (State == BattleState.PreEnd)
            {
                State = BattleState.End;
            }

            onStateChanged?.Invoke(State);
            _stateText.text = State.ToString();
            _resolveState.text = ActionsResolver.I.State.ToString();
        }

        private void DrawCards()
        {
            // todo: implement proper order, later (first p -> first e -> second p -> second e -> ...)
            foreach (var unit in BattleManager.I.Registry.Units.Values)
            {
                unit.DrawCards(TurnNumber == 1);
            }
        }

        private void RollCrystals()
        {
            foreach (var unit in BattleManager.I.Registry.Units.Values)
            {
                foreach (var crystal in unit.UnitCrystals.Crystals)
                {
                    crystal.Roll();
                }
            }
        }

        private void ClearTurn()
        {
            CardTargetsMatrix.I.Clear();
            ActionsResolver.I.ClearAll();
            CardTargetsMatrixUi.I.OnTurnEnd();
            
            foreach (var unit in BattleManager.I.Registry.Units.Values)
            {
                foreach (var crystal in unit.UnitCrystals.Crystals)
                {
                    crystal.OnTurnEnd();
                }
                unit.OnTurnEnd();
            }
        }

        public event Action<BattleState> onStateChanged;
    }
}