using EmberBanner.Core.Enums.Battle;

namespace EmberBanner.Core.Service.Extensions
{
    public static class DamageTypeExtensions
    {
        public static BaseDamageType GetBaseDamageType(this DamageType type) => type switch
        {
            DamageType.Piercing    => BaseDamageType.Physical,
            DamageType.Slashing    => BaseDamageType.Physical,
            DamageType.Crushing    => BaseDamageType.Physical,
                                   
            DamageType.Fire        => BaseDamageType.Natural,
            DamageType.Ice         => BaseDamageType.Natural,
            DamageType.Poison      => BaseDamageType.Natural,
            DamageType.Acid        => BaseDamageType.Natural,
            DamageType.Lightning   => BaseDamageType.Natural,
                                   
            DamageType.Energy      => BaseDamageType.Magic,
            DamageType.Light       => BaseDamageType.Magic,
            DamageType.Dark        => BaseDamageType.Magic,
            DamageType.Mental      => BaseDamageType.Magic,
            
            DamageType.Existential => BaseDamageType.Existential,
        };
    }
}