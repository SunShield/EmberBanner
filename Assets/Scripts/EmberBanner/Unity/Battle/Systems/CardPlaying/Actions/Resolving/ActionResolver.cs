using EmberBanner.Core.Enums.Actions;
using EmberBanner.Core.Ingame.Impl.Battles;
using EmberBanner.Unity.Battle.Views.Impl.Units;

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
        public void ResolveActionPair(BattlePlayingActionEntity initiatorAction, BattlePlayingActionEntity targetAction)
        {
            ResolveAction(initiatorAction);
            
            if (targetAction == null || targetAction.Holder.IsDead) return;
            
            ResolveAction(targetAction);
        }

        private void ResolveAction(BattlePlayingActionEntity action)
        {
            if (action.Model.Type == ActionType.Aggression)
            {
                ResolveAggression(action);
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

        private void ResolveAggression(BattlePlayingActionEntity action)
        {
            
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

        private void AddShield(BattleUnitView unit, int amount)
        {
            unit.Entity.ChangeShield(amount);
        }
        
        private void AddField(BattleUnitView unit, int amount)
        {
            unit.Entity.ChangeField(amount);
        }
        
        private void ResolveSupport(BattlePlayingActionEntity action)
        {
            
        }
    }
}