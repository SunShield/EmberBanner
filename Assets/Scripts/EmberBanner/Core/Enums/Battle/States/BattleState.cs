namespace EmberBanner.Core.Enums.Battle.States
{
    public enum BattleState
    {
        PreStart,
        Start,
        TurnStart,
        
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