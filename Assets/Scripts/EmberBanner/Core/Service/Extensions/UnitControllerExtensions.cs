using EmberBanner.Core.Enums.Battle;

namespace EmberBanner.Core.Service.Extensions
{
    public static class UnitControllerExtensions
    {
        public static UnitControllerType Enemy(this UnitControllerType type) => type switch
        {
            UnitControllerType.Player => UnitControllerType.Enemy,
            UnitControllerType.Enemy => UnitControllerType.Player,
        };
        
        public static UnitControllerType Ally(this UnitControllerType type) => type switch
        {
            UnitControllerType.Player => UnitControllerType.Player,
            UnitControllerType.Enemy => UnitControllerType.Enemy,
        };
    }
}