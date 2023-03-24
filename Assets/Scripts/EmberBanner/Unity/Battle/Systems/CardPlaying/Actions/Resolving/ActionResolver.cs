using EmberBanner.Core.Enums.Actions;
using EmberBanner.Core.Enums.Battle;
using EmberBanner.Core.Ingame.Impl.Battles;
using EmberBanner.Unity.Battle.Systems.CardPlaying.Actions.Resolving.Damaging;
using EmberBanner.Unity.Battle.Views.Impl.Units;
using UnityEngine;

namespace EmberBanner.Unity.Battle.Systems.CardPlaying.Actions.Resolving
{
    public class ActionResolver
    {
        private static ActionResolver _instance;
        public static ActionResolver I => _instance ??= new();

        /// <summary>
        /// Initiator is always quicker, by algorithm's design
        /// </summary>
        /// <param name="initiatorAction"></param>
        /// <param name="targetAction"></param>
        /// <param name="isClash"></param>
        public void ResolveActionIfPossible(BattlePlayingActionEntity initiatorAction, BattlePlayingActionEntity targetAction)
        {
            if (initiatorAction == null || initiatorAction.Holder.IsStaggered || initiatorAction.Holder.IsDead) return;
            
            ResolveAction(initiatorAction, targetAction);
        }

        private void ResolveAction(BattlePlayingActionEntity action, BattlePlayingActionEntity targetAction)
        {
            if (action.Model.Type == ActionType.Aggression)
            {
                ResolveAggression(action, targetAction);
            }
            else if (action.Model.Type == ActionType.Defense)
            {
                ResolveDefense(action);
            }
            else if (action.Model.Type == ActionType.Support)
            {
                ResolveSupport(action);
            }
        }

        private void ResolveAggression(BattlePlayingActionEntity action, BattlePlayingActionEntity targetAction)
        {
            var actionTargetUnit = action.Target.OwnerView;
            var actionMagnitude = action.Magnitude.CalculateValue();
            
            if (action.Model.AggressionType == AggressionType.Damage)
            {
                int? blockAmount = targetAction?.Model.DefenseType == DefenseType.Block
                    ? targetAction.Magnitude.CalculateValue()
                    : null;
                
                DamageUnit(actionTargetUnit, actionMagnitude, blockAmount, action.Model.DamageType);
            }
        }

        private void DamageUnit(BattleUnitView target, int amount, int? blockAmount, DamageType damageType)
        {
            var finalAmount = amount;
            
            // block applied directly to magnitude, before resistances and other stuff
            BlockDamageIfNeeded(ref finalAmount, blockAmount);
            AbsorbDamageWithShieldIfNeeded(ref finalAmount, target);

            UnitDamageCalculator.I.InflictWillDamage(target, finalAmount, damageType);
            UnitDamageCalculator.I.InflictDamage(target, finalAmount, damageType);
        }

        private void BlockDamageIfNeeded(ref int amount, int? blockAmount)
        {
            if (blockAmount != null)
                amount = Mathf.Max(amount - blockAmount.Value, 0);
        }

        private void AbsorbDamageWithShieldIfNeeded(ref int amount, BattleUnitView targetUnit)
        {
            var damageOverShield = Mathf.Max(amount - targetUnit.Entity.CurrentShield, 0);
            targetUnit.Entity.ChangeShield(-amount);
            amount = damageOverShield;
        }

        private void ResolveDefense(BattlePlayingActionEntity action)
        {
            // reactive defense actions are resolved during targeting aggression's resolve
            if (action.Model.DefenseType == DefenseType.Block ||
                action.Model.DefenseType == DefenseType.Barrier)
                return;

            var actionTargetUnit = action.Target.OwnerView;
            var actionMagnitude = action.Magnitude.CalculateValue();
            if (action.Model.DefenseType == DefenseType.Shield)
                AddShield(actionTargetUnit, actionMagnitude); 
            else if (action.Model.DefenseType == DefenseType.Field)
                AddField(actionTargetUnit, actionMagnitude);
        }

        private void AddShield(BattleUnitView target, int amount)
        {
            target.Entity.ChangeShield(amount);
        }
        
        private void AddField(BattleUnitView target, int amount)
        {
            target.Entity.ChangeField(amount);
        }
        
        private void ResolveSupport(BattlePlayingActionEntity action)
        {
            var actionTargetUnit = action.Target.OwnerView;
            var actionMagnitude = action.Magnitude.CalculateValue();
            if (action.Model.SupportType == SupportType.Healing)
                AddHealth(actionTargetUnit, actionMagnitude);
            else if (action.Model.SupportType == SupportType.Stabilization)
                AddWill(actionTargetUnit, actionMagnitude);
        }

        private void AddHealth(BattleUnitView target, int amount)
        {
            target.Entity.ChangeHealth(amount);
        }
        
        private void AddWill(BattleUnitView target, int amount)
        {
            target.Entity.ChangeWill(amount);
        }
    }
}