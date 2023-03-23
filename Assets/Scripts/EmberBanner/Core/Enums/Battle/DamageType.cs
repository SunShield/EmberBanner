namespace EmberBanner.Core.Enums.Battle
{
    /// <summary>
    /// Damage type is a special damage-related tag
    /// It is affected by some battle and out-of-battle effects and also servers flavour purposes
    ///
    ///  
    /// </summary>
    public enum DamageType
    {
        // Physical
        Piercing,
        Slashing,
        Crushing,
        
        // Natural
        Fire,
        Ice,
        Poison,
        Acid,
        Lightning,
        
        // Magic
        Energy,
        Light,
        Dark,
        Mental,
        
        // Existential
        Existential
    }
}