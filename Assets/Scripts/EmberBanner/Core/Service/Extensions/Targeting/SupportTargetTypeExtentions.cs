using EmberBanner.Core.Enums.Battle.Targeting;

namespace EmberBanner.Core.Service.Extensions.Targeting
{
    public static class SupportTargetTypeExtentions
    {
        public static bool AllowsAlly(this SupportTargetType type)
            => type == SupportTargetType.Ally || type == SupportTargetType.AllyOrSelf ;
        
        public static bool AllowsSelf(this SupportTargetType type)
            => type == SupportTargetType.Self || type == SupportTargetType.AllyOrSelf;
    }
}