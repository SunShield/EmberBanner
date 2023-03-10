using System;

namespace EmberBanner.Core.Enums.Battle.Targeting
{
    [Flags]
    public enum SupportTargetType
    {
        Self,
        Ally,
        AllyOrSelf
    }
}