using EmberBanner.Core.Entities.Impl.Cards;
using EmberBanner.Core.Models.Actions;

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
        /// Action can be cancelled via various conditions,
        /// but the main one is being unable to target card's target
        ///
        /// some events during gameplay may uncancel actions
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

        public BattlePlayingActionEntity(CardActionEntity entity) : this(entity.Id, entity.Model)
        {
            CurrentCoinsAmount = CoinsAmount.CalculateValue();
        }
        
        public BattlePlayingActionEntity(int id, ActionModel model) : base(id, model)
        {
        }
    }
}