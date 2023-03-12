using EmberBanner.Core.Entities.Impl.Cards;
using EmberBanner.Core.Models.Actions;
using EmberBanner.Core.Service.Classes.Fundamental;
using EmberBanner.Unity.Battle.Systems.CardPlaying.TurnPlanning;
using EmberBanner.Unity.Battle.Views.Impl.Cards;
using EmberBanner.Unity.Battle.Views.Impl.Units.Crystals;

namespace EmberBanner.Core.Ingame.Impl.Battles
{
    /// <summary>
    /// This class is an action actually taking part in playing process
    ///
    /// Is exists only during actions play phase.
    /// </summary>
    public class BattlePlayingActionEntity : CardActionEntity
    {
        /// <summary>
        /// Crystal holding this action
        /// </summary>
        public BattleUnitCrystalView Holder { get; private set; }
        
        /// <summary>
        /// Card current action is related to
        /// </summary>
        public BattleCardView Card => Holder.Card;
        
        /// <summary>
        /// Crystal action is targeting right now
        /// </summary>
        public BattleUnitCrystalView Target { get; set; }

        public bool TargetIsDead => Target.IsDead;
        public bool IsTargetingSelf => Holder == Target;
        
        /// <summary>
        /// Action can be cancelled via various conditions,
        /// but the main one is being unable to target card's target
        ///
        /// some events during gameplay may un-cancel actions
        /// </summary>
        public bool IsCancelled { get; set; }
        
        /// <summary>
        /// Distraction is a special state action suffers
        /// losing clashed or receiving one-sided attack
        /// </summary>
        public bool IsDistracted { get; set; }
        
        /// <summary>
        /// Coins amount persists between different action activations during the same turn
        /// </summary>
        public int CurrentCoinsAmount { get; set; }

        /// <summary>
        /// Final clashing bounds are affected by crystal's speed
        /// </summary>
        public IntSpan FinalClashingBounds { get; set; }

        public BattlePlayingActionEntity(CardActionEntity entity, BattleUnitCrystalView boundCrystal) : this(entity.Id, entity.Model)
        {
            var clashingBoundsIncreasement = (boundCrystal.CurrentRoll.Value / entity.ClashingPowerGrowthSpan.CalculateValue()) * entity.ClashingPowerGrowthRate.CalculateValue();
            FinalClashingBounds = new IntSpan();
            FinalClashingBounds.Min = entity.MinClashingPower.CalculateValue() + clashingBoundsIncreasement;
            FinalClashingBounds.Max = entity.MaxClashingPower.CalculateValue() + clashingBoundsIncreasement;
            
            CurrentCoinsAmount = CoinsAmount.CalculateValue();
        }
        
        public BattlePlayingActionEntity(int id, ActionModel model) : base(id, model)
        {
        }
    }
}