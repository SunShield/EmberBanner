using EmberBanner.Core.Enums.Battle.Targeting;

namespace EmberBanner.Core.Service.Extensions.Targeting
{
    public static class AggressionTargetTypeExtentions
    {
        public static bool AllowsEnemy(this AggressionTargetType type)
            => type == AggressionTargetType.Enemy || type == AggressionTargetType.EnemyOrSelf ;
        
        public static bool AllowsSelf(this AggressionTargetType type)
            => type == AggressionTargetType.Self || type == AggressionTargetType.EnemyOrSelf;
    }
}