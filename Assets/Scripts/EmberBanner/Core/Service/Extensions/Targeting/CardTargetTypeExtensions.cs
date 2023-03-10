using EmberBanner.Core.Enums.Battle.Targeting;

namespace EmberBanner.Core.Service.Extensions.Targeting
{
    public static class CardTargetTypeExtensions
    {
        public static bool AllowsEnemy(this CardTargetType type)
        {
            return type == CardTargetType.Enemy || type == CardTargetType.EnemyOrSelf ||
                   type == CardTargetType.AllyOrEnemy || type == CardTargetType.Anyone;
        }
        
        public static bool AllowsAlly(this CardTargetType type)
        {
            return type == CardTargetType.Ally || type == CardTargetType.AllyOrSelf ||
                   type == CardTargetType.AllyOrEnemy || type == CardTargetType.Anyone;
        }
        
        public static bool AllowsSelf(this CardTargetType type)
        {
            return type == CardTargetType.Self || type == CardTargetType.AllyOrSelf ||
                   type == CardTargetType.EnemyOrSelf || type == CardTargetType.Anyone;
        }
    }
}