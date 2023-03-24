using EmberBanner.Core.Enums.Battle;
using EmberBanner.Core.Service.Extensions;
using EmberBanner.Unity.Battle.Views.Impl.Units;
using UnityEngine;

namespace EmberBanner.Unity.Battle.Systems.CardPlaying.Actions.Resolving.Damaging
{
    public class UnitDamageCalculator
    {
        private static UnitDamageCalculator _instance;
        public static UnitDamageCalculator I => _instance ??= new();

        public (int finalDamage, bool isLethal) InflictDamage(BattleUnitView target, int outgoingDamage, DamageType damageType)
        {
            var isUnitDead = target.IsDead;
            if (isUnitDead) return (0, false);
            
            var baseDamageType = damageType.GetBaseDamageType();
            var resistance = GetMatchingResistance(target, baseDamageType);
            var damageMultiplier = GetResistanceMultiplier(resistance);
            var incomingDamage = (int)(damageMultiplier * outgoingDamage);
            var finalIncomingDamage = GetFinalIncomingDamage(incomingDamage, target);
            ApplyDamageToTarget(target, incomingDamage);
            return (finalIncomingDamage, target.IsDead);
        }

        private int GetMatchingResistance(BattleUnitView unit, BaseDamageType damageType) => damageType switch
        {
            BaseDamageType.Physical    => unit.Entity.PhysicalResistance.CalculateValue(),
            BaseDamageType.Natural     => unit.Entity.NaturalResistance.CalculateValue(),
            BaseDamageType.Magic       => unit.Entity.MagicResistance.CalculateValue(),
            BaseDamageType.Existential => 0,
        };

        private float GetResistanceMultiplier(int resistance) => 1f - resistance / 100f;

        // some effects rely on the exact portion of damage dealt to unit
        // damage dealt can't exceed unit's health
        private int GetFinalIncomingDamage(int incomingDamage, BattleUnitView targetUnit) => Mathf.Min(incomingDamage, targetUnit.Entity.CurrentHealth);
        
        private void ApplyDamageToTarget(BattleUnitView targetUnit, int incomingDamage) => targetUnit.Entity.ChangeHealth(-incomingDamage);
        
        public (int finalDamage, bool isStaggering) InflictWillDamage(BattleUnitView target, int outgoingDamage, DamageType damageType)
        {
            var isUnitDead = target.IsDead;
            var isUnitStaggered = target.IsStaggered;
            if (isUnitStaggered || isUnitDead) return (0, false);
            
            var baseDamageType = damageType.GetBaseDamageType();
            var resistance = GetMatchingWillResistance(target, baseDamageType);
            var damageMultiplier = GetResistanceMultiplier(resistance);
            var incomingDamage = (int)(damageMultiplier * outgoingDamage);
            var finalIncomingDamage = GetFinalWillIncomingDamage(incomingDamage, target);
            ApplyWillDamageToTarget(target, incomingDamage);
            return (finalIncomingDamage, target.IsDead);
        }
        
        private int GetMatchingWillResistance(BattleUnitView unit, BaseDamageType damageType) => damageType switch
        {
            BaseDamageType.Physical    => unit.Entity.WillPhysicalResistance.CalculateValue(),
            BaseDamageType.Natural     => unit.Entity.WillNaturalResistance.CalculateValue(),
            BaseDamageType.Magic       => unit.Entity.WillMagicResistance.CalculateValue(),
            BaseDamageType.Existential => 0,
        };
        
        // some effects rely on the exact portion of will damage dealt to unit
        // damage dealt can't exceed unit's will
        private int GetFinalWillIncomingDamage(int incomingDamage, BattleUnitView targetUnit) => Mathf.Min(incomingDamage, targetUnit.Entity.CurrentWill);
        
        private void ApplyWillDamageToTarget(BattleUnitView targetUnit, int incomingDamage) => targetUnit.Entity.ChangeWill(-incomingDamage);
    }
}