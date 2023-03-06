using EmberBanner.Core.Enums.Battle.States;
using EmberBanner.Unity.Battle.Management;
using EmberBanner.Unity.Service;

namespace EmberBanner.Unity.Battle.Systems.StateSystem
{
    public class BattleStateController : EBMonoBehaviour
    {
        public BattleState State { get; private set; } = BattleState.PreStart;

        public void MoveState()
        {
            if (State == BattleState.PreStart)
            {
                State = BattleState.Start;
            }
            else if (State == BattleState.Start)
            {
                State = BattleState.TurnStart;
            }
            else if (State == BattleState.TurnStart)
            {
                BattleManager.I.TurnOrderController.DetermineTurnOrder();
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
        }
    }
}