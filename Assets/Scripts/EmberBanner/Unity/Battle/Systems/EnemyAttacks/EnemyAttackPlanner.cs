using System.Collections.Generic;
using System.Linq;
using EmberBanner.Core.Enums.Battle;
using EmberBanner.Core.Enums.Battle.Targeting;
using EmberBanner.Core.Service.Extensions;
using EmberBanner.Core.Service.Extensions.Targeting;
using EmberBanner.Unity.Battle.Management;
using EmberBanner.Unity.Battle.Systems.CardPlaying.TurnPlanning;
using EmberBanner.Unity.Battle.Views.Impl.Cards;
using EmberBanner.Unity.Battle.Views.Impl.Units;
using EmberBanner.Unity.Battle.Views.Impl.Units.Crystals;
using UnityEngine;

namespace EmberBanner.Unity.Battle.Systems.EnemyAttacks
{
    public class EnemyAttackPlanner
    {
        private static EnemyAttackPlanner _instance;
        public static EnemyAttackPlanner I => _instance ??= new();

        public void SetEnemyAttacks()
        {
            // By default, random units are targeted
            // But, possibly, later some enemies will have primitive AI

            foreach (var enemy in BattleManager.I.Registry.EnemyUnits.Values)
            {
                foreach (var enemyCrystal in enemy.UnitCrystals.Crystals)
                {
                    var cards = enemyCrystal.OwnerView.CardsInHand;
                    var availableCards = cards.Where(c => c.CanBePlayed() && CheckCardHasLegalTargets(c)).ToList();
                    
                    if (availableCards.Count == 0) break;
                    var randomCardIndex = Random.Range(0, availableCards.Count);
                    var randomCard = availableCards[randomCardIndex];
                    
                    if (randomCard.Model.TargetType == TargetType.Self)
                        CardPrePlayManager.I.SetCardPrePlayed(randomCard, enemyCrystal);
                    else
                    {
                        var legalTargets = GetCardLegalTargets(enemy, randomCard);
                        var randomTargetIndex = Random.Range(0, legalTargets.Count);
                        var legalTarget = legalTargets[randomTargetIndex];
                        CardPrePlayManager.I.SetCardPrePlayedWithTarget(randomCard, enemyCrystal, legalTarget, true);
                    }
                }
            }
        }

        private bool CheckCardHasLegalTargets(BattleCardView card)
        {
            if (card.Model.TargetType.AllowsSelf()) return true;
            if (card.Model.TargetType.AllowsEnemy())
            {
                foreach (var playerUnit in BattleManager.I.Registry.PlayerUnits.Values)
                {
                    foreach (var crystal in playerUnit.UnitCrystals.Crystals)
                    {
                        if (crystal.CanBeTargeted(card)) return true;
                    }
                }
            }
            if (card.Model.TargetType.AllowsAlly())
            {
                foreach (var enemyUnit in BattleManager.I.Registry.EnemyUnits.Values)
                {
                    foreach (var crystal in enemyUnit.UnitCrystals.Crystals)
                    {
                        if (crystal.CanBeTargeted(card)) return true;
                    }
                }
            }

            return false;
        }

        private List<BattleUnitCrystalView> GetCardLegalTargets(BattleUnitView enemy, BattleCardView card)
        {
            var legalTargets = new List<BattleUnitCrystalView>();
            if (card.Model.TargetType.AllowsEnemy())
            {
                foreach (var playerUnit in BattleManager.I.Registry.PlayerUnits.Values)
                {
                    legalTargets.AddRange(playerUnit.UnitCrystals.Crystals.Where(c => c.CanBeTargeted(card)));
                }
            }
            if (card.Model.TargetType.AllowsAlly())
            {
                foreach (var enemyUnit in BattleManager.I.Registry.EnemyUnits.Values)
                {
                    legalTargets.AddRange(enemyUnit.UnitCrystals.Crystals.Where(c => c.CanBeTargeted(card)));
                }
            }
            if (card.Model.TargetType.AllowsSelf())
            {
                legalTargets.AddRange(enemy.UnitCrystals.Crystals.Where(c => c.CanBeTargeted(card)));
            }

            return legalTargets;
        }
    }
}