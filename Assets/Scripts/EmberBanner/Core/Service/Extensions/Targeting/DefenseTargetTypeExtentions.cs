using EmberBanner.Core.Enums.Battle.Targeting;

namespace EmberBanner.Core.Service.Extensions.Targeting
{
    public static class DefenseTargetTypeExtentions
    {
        public static bool AllowsEnemy(this DefenseTargetType type)
            => type == DefenseTargetType.Enemy || type == DefenseTargetType.EnemyOrSelf || type == DefenseTargetType.AllyOrEnemy || type == DefenseTargetType.Anyone;
        
        public static bool AllowsAlly(this DefenseTargetType type)
            => type == DefenseTargetType.Ally || type == DefenseTargetType.AllyOrEnemy || type == DefenseTargetType.AllyOrSelf || type == DefenseTargetType.Anyone;
        
        public static bool AllowsSelf(this DefenseTargetType type)
            => type == DefenseTargetType.Self || type == DefenseTargetType.AllyOrSelf || 
               type == DefenseTargetType.EnemyOrSelf || type == DefenseTargetType.Anyone;
    }
}