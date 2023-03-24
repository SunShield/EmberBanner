using System.Collections.Generic;
using System.Linq;
using EmberBanner.Core.Enums.Actions;
using EmberBanner.Core.Ingame.Impl.Battles;
using EmberBanner.Unity.Battle.Systems.CardPlaying.TurnPlanning;
using EmberBanner.Unity.Battle.Views.Impl.Units.Crystals;

namespace EmberBanner.Unity.Battle.Systems.CardPlaying.Actions
{
    /// <summary>
    /// Logic behind choosing a proper action (PrA) is not simple
    /// Initiator's action is I-A and target action is T-A
    /// Initiator's proper action is PrA-I and target's proper action is PrA-T
    ///
    /// ("unit's ally" here refers to a unit with same same controller as the initiator, but NOT unit itself)
    ///
    /// ╔═ ██ Is attack a Clash? ██
    /// ╠═ [NO] ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓
    /// ║  ┃ ╔═ ██ Is I-A top action a non-Self Aggression? ██                               ┃
    /// ║  ┃ ╠═ [NO] ━━━━━━━━━━━━━━━┓                                                        ┃
    /// ║  ┃ ║  ┃ There is no PrA-T ┃                                                        ┃
    /// ║  ┃ ║  ┗━━━━━━━━━━━━━━━━━━━┛                                                        ┃
    /// ║  ┃ ╚═ [YES] ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓ ┃
    /// ║  ┃    ┃ PrA-I is I-A                                                             ┃ ┃
    /// ║  ┃    ┃ ╔═ ██ Does Target have self-Aggression or self-reactive-Defense          ┃ ┃
    /// ║  ┃    ┃ ║     not targeting initiator's ally? ██                                 ┃ ┃
    /// ║  ┃    ┃ ╠═ [NO] ━━━━━━━━━━━━━━━┓                                                 ┃ ┃
    /// ║  ┃    ┃ ║  ┃ There is no PrA-T ┃                                                 ┃ ┃
    /// ║  ┃    ┃ ║  ┗━━━━━━━━━━━━━━━━━━━┛                                                 ┃ ┃
    /// ║  ┃    ┃ ╚═ [YES] ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓ ┃ ┃
    /// ║  ┃    ┃    ┃ PrA-T is first self-Aggression or self-reactive-Defense           ┃ ┃ ┃
    /// ║  ┃    ┃    ┃ not targeting initiator's ally                                    ┃ ┃ ┃
    /// ║  ┃    ┃    ┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛ ┃ ┃
    /// ║  ┃    ┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛ ┃
    /// ║  ┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛
    /// ╚═ [YES] ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓
    ///    ┃  ╔═ ██ Is I-A action a non-Self Aggression? ██                                              ┃   
    ///    ┃  ╠═ [NO] ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓ ┃ 
    ///    ┃  ║  ┃ ╔═ ██ Is T-A a non-self Aggression? ██                                              ┃ ┃
    ///    ┃  ║  ┃ ╠═ [NO] ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓                         ┃ ┃
    ///    ┃  ║  ┃ ║  ┃ PrA-I and PrA-T both are first active action or none ┃                         ┃ ┃
    ///    ┃  ║  ┃ ║  ┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛                         ┃ ┃
    ///    ┃  ║  ┃ ╚═ [YES] ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓ ┃ ┃
    ///    ┃  ║  ┃    ┃ PrA-T is T-A                                                                 ┃ ┃ ┃
    ///    ┃  ║  ┃    ┃ ╔═ ██ Does Initiator have self-Aggression or self-reactive-Defense? ██       ┃ ┃ ┃
    ///    ┃  ║  ┃    ┃ ╠═ [NO] ━━━━━━━━━━━━━━━━━━┓                                                  ┃ ┃ ┃
    ///    ┃  ║  ┃    ┃ ║  ┃ PrA-I is I-A or none ┃                                                  ┃ ┃ ┃
    ///    ┃  ║  ┃    ┃ ║  ┗━━━━━━━━━━━━━━━━━━━━━━┛                                                  ┃ ┃ ┃
    ///    ┃  ║  ┃    ┃ ╚═ [YES] ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓ ┃ ┃ ┃
    ///    ┃  ║  ┃    ┃     ┃ PrA-I is first self-Aggression or self-reactive-Defense of Initiator ┃ ┃ ┃ ┃
    ///    ┃  ║  ┃    ┃     ┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛ ┃ ┃ ┃
    ///    ┃  ║  ┃    ┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛ ┃ ┃
    ///    ┃  ║  ┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛ ┃ 
    ///    ┃  ╚═ [YES] ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓     ┃
    ///    ┃     ┃ ╔═ ██ Is T-A a non-self Aggression? ██                                          ┃     ┃
    ///    ┃     ┃ ╠═ [YES] ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓                             ┃     ┃
    ///    ┃     ┃ ║  ┃ PrA-I and PrA-T both are first active action ┃                             ┃     ┃
    ///    ┃     ┃ ║  ┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛                             ┃     ┃
    ///    ┃     ┃ ╚═ [NO] ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓ ┃     ┃
    ///    ┃     ┃    ┃ ╔═ ██ Does Target have self-Aggression or self-reactive-Defense? ██      ┃ ┃     ┃
    ///    ┃     ┃    ┃ ╠═ [NO] ━━━━━━━━━━━━━━━━━━┓                                              ┃ ┃     ┃
    ///    ┃     ┃    ┃ ║  ┃ PrA-T is I-A or none ┃                                              ┃ ┃     ┃
    ///    ┃     ┃    ┃ ║  ┗━━━━━━━━━━━━━━━━━━━━━━┛                                              ┃ ┃     ┃
    ///    ┃     ┃    ┃ ╚═ [YES] ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓ ┃ ┃     ┃
    ///    ┃     ┃    ┃    ┃ PrA-T is first self-Aggression or self-reactive-Defense of Target ┃ ┃ ┃     ┃
    ///    ┃     ┃    ┃    ┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛ ┃ ┃     ┃
    ///    ┃     ┃    ┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛ ┃     ┃
    ///    ┃     ┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛     ┃
    ///    ┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛
    public class CrystalProperActionSelector
    {
        private static CrystalProperActionSelector _instance;
        public static CrystalProperActionSelector I => _instance ??= new();

        public (BattlePlayingActionEntity initiatorAction, BattlePlayingActionEntity targetAction) FindProperActions(BattleUnitCrystalView initiator, BattleUnitCrystalView target)
        {
            BattlePlayingActionEntity properInitiatorAction = null;
            BattlePlayingActionEntity properTargetAction = null;
            var initiatorNonCancelledActions = GetNonCancelledActions(initiator);
            var targetNonCancelledActions = GetNonCancelledActions(target);

            var isTargetingSelf = CardTargetsMatrix.I.CheckTargetsSelf(initiator);
            if (isTargetingSelf) return (FindFirstSelfPlayableAction(initiatorNonCancelledActions), null);
            
            var isClash = CardTargetsMatrix.I.CheckClash(initiator, target);
            if (!isClash)
            {
                properInitiatorAction = FindFirstNonReactiveAction(initiatorNonCancelledActions);

                // If during one-sided attack Aggression was initiator's proper action, target can respond on it
                // with reactive action
                if (properInitiatorAction?.Model.Type == ActionType.Aggression)
                    properTargetAction = FindFirstReactiveActionAgainstInitiator(targetNonCancelledActions, properInitiatorAction);
            }
            else
            {
                properInitiatorAction = FindFirstNonReactiveAction(initiatorNonCancelledActions);
                if (properInitiatorAction?.Model.Type == ActionType.Aggression)
                    properTargetAction = FindFirstClashableActionAgainstInitiatorsAggression(targetNonCancelledActions, properInitiatorAction);
                else
                {
                    properTargetAction = FindFirstNonReactiveAction(targetNonCancelledActions);
                    if (properTargetAction?.Model.Type == ActionType.Aggression)
                    {
                        var possibleReactiveAction = FindFirstReactiveActionAgainstInitiator(initiatorNonCancelledActions, properTargetAction);
                        if (possibleReactiveAction != null)
                            properInitiatorAction = possibleReactiveAction;
                    }
                }
            }
            
            return (properInitiatorAction, properTargetAction);
        }

        private BattlePlayingActionEntity FindFirstSelfPlayableAction(List<BattlePlayingActionEntity> actions)
            => actions.FirstOrDefault(action => action.IsSelfPlayable());

        private BattlePlayingActionEntity FindFirstNonReactiveAction(List<BattlePlayingActionEntity> actions)
            => actions.FirstOrDefault(action => !action.IsReactive());
        
        private BattlePlayingActionEntity FindFirstReactiveActionAgainstInitiator(List<BattlePlayingActionEntity> actions, BattlePlayingActionEntity initiator)
            => actions.FirstOrDefault(action => !action.IsNotReactiveAgainstTarget(initiator));
        
        private BattlePlayingActionEntity FindFirstClashableActionAgainstInitiatorsAggression(List<BattlePlayingActionEntity> actions, BattlePlayingActionEntity initiator)
            => actions.FirstOrDefault(action => !action.IsNotReactiveAgainstTarget(initiator) || action.Model.Type == ActionType.Aggression);

        private List<BattlePlayingActionEntity> GetNonCancelledActions(BattleUnitCrystalView crystal) =>
            crystal.Actions.Where(a => !a.IsCancelled).ToList();

        /*public BattlePlayingActionEntity FindProperTargetAction(BattlePlayingActionEntity initiatingAction, BattleUnitCrystalView target)
        {
            var targetNonCancelledActions = GetNonCancelledActions(target);
            var isClash = CardTargetsMatrix.I.CheckClash(initiatingAction.Holder, target);

            if (!isClash)
            {
                if (initiatingAction.Model.Type == ActionType.Aggression)
                    return FindFirstReactiveActionAgainstInitiator(targetNonCancelledActions, initiatingAction.Holder);
            }
            else
            {
                return initiatingAction.Model.Type == ActionType.Aggression 
                    ? FindFirstClashableActionAgainstInitiatorsAggression(targetNonCancelledActions, initiatingAction.Holder) 
                    : FindFirstNonReactiveAction(targetNonCancelledActions);
            }

            return null;
        }*/
    }
}