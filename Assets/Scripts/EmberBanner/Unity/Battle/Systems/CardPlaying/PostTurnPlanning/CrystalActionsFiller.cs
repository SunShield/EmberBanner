using System.Linq;
using EmberBanner.Core.Entities.Impl.Cards;
using EmberBanner.Core.Ingame.Impl.Battles;
using EmberBanner.Core.Service.Extensions;
using EmberBanner.Core.Service.Extensions.Targeting;
using EmberBanner.Unity.Battle.Management;
using EmberBanner.Unity.Battle.Systems.CardPlaying.TurnPlanning;
using EmberBanner.Unity.Battle.Views.Impl.Cards;
using EmberBanner.Unity.Battle.Views.Impl.Units.Crystals;

namespace EmberBanner.Unity.Battle.Systems.CardPlaying.PostTurnPlanning
{
    public class CrystalActionsFiller
    {
        private static CrystalActionsFiller _instance;
        public static CrystalActionsFiller I => _instance ??= new();

        public void AddActionsToCrystals()
        {
            foreach (var unit in BattleManager.I.Registry.Units.Values)
            {
                foreach (var crystal in unit.UnitCrystals.Crystals)
                {
                    crystal.SetNonCancelledActionsAmount(0);
                    if (crystal.Card == null) continue;
                    
                    crystal.AddActions(crystal.Card.Entity.Actions.Select(action => CreatePlayingAction(crystal.Card, action)).ToList());
                    crystal.SetNonCancelledActionsAmount(crystal.Actions.Count(a => !a.IsCancelled));
                }
            }
        }

        private BattlePlayingActionEntity CreatePlayingAction(BattleCardView card, CardActionEntity action)
        {
            var playingAction = new BattlePlayingActionEntity(action, card.Crystal)
            {
                IsCancelled = DetermineIfActionIsCancelled(card, action)
            };
            if (!playingAction.IsCancelled)
                playingAction.Target = DetermineActionTarget(playingAction);

            return playingAction;
        }

        /// <summary>
        /// Algorithm of determining action cancel state is the following:
        ///
        /// 1. If card targets crystal action is allowed to target, action is not cancelled (card allows enemy, action allows enemy, etc)
        /// 2. Actions able to target SELF are never cancelled. In contrast, actions unable to target self are cancelled if they are not targeting their determined target
        /// </summary>
        /// <returns></returns>
        private bool DetermineIfActionIsCancelled(BattleCardView card, CardActionEntity action)
        {
            var actionTargetType = action.Model.TargetType;
            if (actionTargetType.AllowsSelf()) return false;
            
            var initiatorCrystal = card.Crystal;
            var cardTarget = CardTargetsMatrix.I.GetTarget(initiatorCrystal);
            if (cardTarget == initiatorCrystal) return true; // card targets self but action is not allowed to target self
            if (cardTarget.Controller == initiatorCrystal.Controller         && actionTargetType.AllowsAlly())  return false;
            if (cardTarget.Controller == initiatorCrystal.Controller.Enemy() && actionTargetType.AllowsEnemy()) return false;

            return true;
        }

        private BattleUnitCrystalView DetermineActionTarget(BattlePlayingActionEntity action)
        {
            var actionTargetType = action.Model.TargetType;
            var crystalTarget = CardTargetsMatrix.I.GetTarget(action.Holder);

            // If action does not target self, but can do it...
            if (crystalTarget != action.Holder && actionTargetType.AllowsSelf())
            {
                // ...and cannot target the crystal target, action always targets self
                if (crystalTarget.Controller == action.Holder.Controller.Enemy() && !actionTargetType.AllowsEnemy() ||
                    crystalTarget.Controller == action.Holder.Controller.Ally() && !actionTargetType.AllowsAlly())
                    return action.Holder;
            }
            
            if (crystalTarget == action.Holder && actionTargetType.AllowsSelf() ||
                crystalTarget.Controller == action.Holder.Controller.Enemy() && actionTargetType.AllowsEnemy() ||
                crystalTarget.Controller == action.Holder.Controller.Ally() && actionTargetType.AllowsAlly())
                return crystalTarget;
            
            return null;
        }
    }
}