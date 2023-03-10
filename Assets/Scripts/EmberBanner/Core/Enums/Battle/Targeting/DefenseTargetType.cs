using System;

namespace EmberBanner.Core.Enums.Battle.Targeting
{
    [Flags]
    public enum DefenseTargetType
    {
        Self,   // self
        Ally,
        AllyOrSelf,
        Enemy,
        EnemyOrSelf,
        AllyOrEnemy,
        Anyone
    }
}