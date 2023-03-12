﻿using System.Collections.Generic;
using EmberBanner.Core.Enums.Battle;
using EmberBanner.Core.Ingame.Impl.Battles;
using EmberBanner.Core.Models.Units;
using EmberBanner.Unity.Battle.Systems.CardZonesSystem;
using EmberBanner.Unity.Battle.Systems.UnitSpotSystem;
using EmberBanner.Unity.Battle.Views.Impl.Cards;
using EmberBanner.Unity.Battle.Views.Impl.Units.Crystals;
using UnityEngine;
using BattleUnitCrystalView = EmberBanner.Unity.Battle.Views.Impl.Units.Crystals.BattleUnitCrystalView;

namespace EmberBanner.Unity.Battle.Views.Impl.Units
{
    public class BattleUnitView : BattleView<BattleUnitEntity, UnitModel>
    {
        private const float SpotSize = 64f;
        
        [SerializeField] private SpriteRenderer _graphics;
        [SerializeField] private BattleUnitCrystalsView _unitCrystals;
        private UnitCardZonesManager _zonesManager;

        public UnitSpot Spot { get; private set; }
        
        public UnitControllerType Controller => Entity.Controller;
        public BattleUnitCrystalsView UnitCrystals => _unitCrystals;
        public List<BattleCardView> CardsInHand => _zonesManager.CardsInHand;
        
        public bool IsDead { get; private set; }

        public void SetCrystals(List<BattleUnitCrystalView> crystals) => _unitCrystals.SetCrystals(crystals);

        protected override void PostInitialize()
        {
            SetGraphics();
        }

        private void SetGraphics()
        {
            _graphics.sprite = Entity.Model.Sprite;
            var spriteSize = Entity.Model.Sprite.texture.width;
            var graphicsScale = SpotSize / spriteSize * 2;
            _graphics.transform.localScale =
                new Vector3((Controller == UnitControllerType.Player ? 1f : -1f) * graphicsScale, graphicsScale, 1f);
        }

        public void SetZonesManager(UnitCardZonesManager zonesManager)
        {
            _zonesManager = zonesManager;
        }

        public void SetSpot(UnitSpot spot) => Spot = spot;

        public void SetZonesActive(bool active) => _zonesManager.SetActive(active);

        public void DrawCards(bool isFirstTurn)
        {
            if (isFirstTurn) _zonesManager.DrawCardsAtBattleStart();
            else             _zonesManager.DrawCardsAtTurnStart();
        }

        public bool CanPlayCard(BattleCardView card)
        {
            return card.Entity.Cost.CalculateValue() <= Entity.CurrentEnergy;
        }

        public void PayCard(BattleCardView card)
        {
            Entity.CurrentEnergy -= card.Entity.Cost.CalculateValue();
        }

        public void UnpayCard(BattleCardView card)
        {
            Entity.CurrentEnergy += card.Entity.Cost.CalculateValue();
        }

        public void SetCardPrePlayed(BattleCardView card, BattleUnitCrystalView crystal) => _zonesManager.SetCardPrePlayed(card, crystal);
        public void UnsetCardPrePlayed(BattleCardView card, BattleUnitCrystalView crystal) => _zonesManager.UnsetCardPrePlayed(card, crystal);
    }
}