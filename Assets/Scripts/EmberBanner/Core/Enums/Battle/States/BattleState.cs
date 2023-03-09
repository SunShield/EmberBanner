namespace EmberBanner.Core.Enums.Battle.States
{
    public enum BattleState
    {
        PreStart,
        Start,
        TurnStart,
        TurnPlan, // selecting cards and targets here
        
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