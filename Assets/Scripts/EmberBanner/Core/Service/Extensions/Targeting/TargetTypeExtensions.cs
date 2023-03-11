using EmberBanner.Core.Enums.Battle.Targeting;

namespace EmberBanner.Core.Service.Extensions.Targeting
{
    public static class TargetTypeExtensions
    {
        public static bool AllowsEnemy(this TargetType type)
        {
            return type == TargetType.Enemy || type == TargetType.EnemyOrSelf ||
                   type == TargetType.AllyOrEnemy || type == TargetType.Anyone;
        }
        
        public static bool AllowsAlly(this TargetType type)
        {
            return type == TargetType.Ally || type == TargetType.AllyOrSelf ||
                   type == TargetType.AllyOrEnemy || type == TargetType.Anyone;
        }
        
        public static bool AllowsSelf(this TargetType type)
        {
            return type == TargetType.Self || type == TargetType.AllyOrSelf ||
                   type == TargetType.EnemyOrSelf || type == TargetType.Anyone;
        }
    }
}