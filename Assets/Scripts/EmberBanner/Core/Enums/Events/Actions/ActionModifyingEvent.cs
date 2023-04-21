namespace EmberBanner.Core.Enums.Events.Actions
{
    public enum ActionModifyingEvent
    {
        // Changes coins amount after action starts resolving first time but before resolving itself
        OnPreGetCoinsAmount,
        
        // Modifies clashing roll bounds
        OnPreCalculateClashingBounds,
        OnPreCalculateFinalClashingPower,
        OnPreCalculateClashLosePenalty,
        OnPreCalculateMagnitude
    }
}