namespace EmberBanner.Core.Enums.Battle.States
{
    public enum BattleState
    {
        PreStart,
        Start,
        TurnStart,
        TurnPlan, // selecting cards and targets here
        PostTurnPlan, // pre-execution battlefield state is calculated here
        
        //// Player repeatedly for each unit
        /**/ CrystalTurnStart,
        /**/ CrustalTurn,
        /**/ CrystalTurnEnd,
        ////////////////////////////////////
        
        TurnEnd,
        PreEnd,
        End
    }
}