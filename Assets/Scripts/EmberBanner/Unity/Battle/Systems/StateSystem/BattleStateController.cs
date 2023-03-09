using EmberBanner.Core.Enums.Battle.States;
using EmberBanner.Unity.Battle.Management;
using EmberBanner.Unity.Battle.Systems.Startup;
using EmberBanner.Unity.Service;
using TMPro;
using UnityEngine;

namespace EmberBanner.Unity.Battle.Systems.StateSystem
{
    public class BattleStateController : EBMonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _stateText;
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
                BattleManager.I.TurnOrderController.DetermineTurnOrder();
                DrawCards();
                State = BattleState.CrystalTurnStart;
            }
            else if (State == BattleState.CrystalTurnStart)
            {
                State = BattleState.CrustalTurn;
            }
            else if (State == BattleState.CrustalTurn)
            {
                State = BattleState.CrystalTurnEnd;
            }
            else if (State == BattleState.CrystalTurnEnd)
            {
                if (BattleManager.I.TurnOrderController.AllCrystalsEndedTurns)
                    State = BattleState.TurnEnd;
                else
                {
                    BattleManager.I.TurnOrderController.AdvanceOrder();
                    State = BattleState.CrystalTurnStart;
                }
            }
            else if (State == BattleState.TurnEnd)
            {
                State = BattleState.CrystalTurnStart;
            }
            else if (State == BattleState.PreEnd)
            {
                State = BattleState.End;
            }

            _stateText.text = State.ToString();
        }

        private void DrawCards()
        {
            // todo: implement proper order, later (first p -> first e -> second p -> second e -> ...)
            foreach (var unit in BattleManager.I.Registry.Units.Values)
            {
                unit.DrawCards(TurnNumber == 1);
            }
        }
    }
}