using System;
using System.Collections.Generic;
using EmberBanner.Core.Entities.Impl.Cards;
using EmberBanner.Core.Models.Actions;
using EmberBanner.Core.Service.Classes.Fundamental;
using EmberBanner.Unity.Battle.Views.Impl.Cards;
using EmberBanner.Unity.Battle.Views.Impl.Units.Crystals;
using Random = UnityEngine.Random;

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
        /// Previous coin results
        /// </summary>
        public List<bool> CoinResults { get; set; } = new();

        public int CurrentCoinsAmount => CoinsAmount.CalculateValue() - CoinResults.Count;

        public int? CurrentRoll { get; set; }

        public BattlePlayingActionEntity(CardActionEntity entity, BattleUnitCrystalView holder) : this(entity.Id, entity.Model)
        {
            Holder = holder;
        }
        
        public BattlePlayingActionEntity(int id, ActionModel model) : base(id, model)
        {
        }

        public void Roll() => CurrentRoll = Random.Range(MinClashingPower.CalculateValue(), MaxClashingPower.CalculateValue() + 1);
    }
}