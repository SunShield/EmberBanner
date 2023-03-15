using System.Collections.Generic;
using EmberBanner.Core.Enums.Battle;
using EmberBanner.Core.Ingame.Impl.Battles;
using EmberBanner.Core.Models.Units;
using EmberBanner.Unity.Battle.Systems.CardZonesSystem;
using EmberBanner.Unity.Battle.Systems.UnitSpotSystem;
using EmberBanner.Unity.Battle.Views.Impl.Cards;
using TMPro;
using UnityEngine;
using BattleUnitCrystalView = EmberBanner.Unity.Battle.Views.Impl.Units.Crystals.BattleUnitCrystalView;

namespace EmberBanner.Unity.Battle.Views.Impl.Units
{
    public class BattleUnitView : BattleView<BattleUnitEntity, UnitModel>
    {
        [SerializeField] private SpriteRenderer _graphics;
        [SerializeField] private BattleUnitCrystalsView _unitCrystals;
        [SerializeField] private Transform _unitHealthBar;
        [SerializeField] private TextMeshPro _unitHealthText;
        [SerializeField] private Transform _unitWillBar;
        [SerializeField] private TextMeshPro _unitWillText;
        [SerializeField] private TextMeshPro _unitEnergyText;
        [SerializeField] private TextMeshPro _unitShieldText;
        [SerializeField] private TextMeshPro _unitFieldText;
        
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
            if (Controller == UnitControllerType.Enemy)
                _graphics.transform.localScale = new(-1f, 1f, 1f);
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

        public void OnTurnEnd()
        {
            foreach (var crystal in _unitCrystals.Crystals)
            {
                crystal.OnTurnEnd();
                if (crystal.Card == null) continue;

                var card = crystal.Card;
                _zonesManager.UnsetCardPrePlayed(crystal.Card, crystal);
                _zonesManager.MoveToGraveyard(card);
            }
        }

        private void Update()
        {
            _unitHealthBar.localScale = new Vector3((float)Entity.CurrentHealth / Entity.MaxHealth.CalculateValue(), 1f, 1f);
            _unitHealthText.text = $"{Entity.CurrentHealth}/{Entity.MaxHealth.CalculateValue()}";
            
            _unitWillBar.localScale = new Vector3((float)Entity.CurrentWill / Entity.MaxWill.CalculateValue(), 1f, 1f);
            _unitWillText.text = $"{Entity.CurrentWill}/{Entity.MaxWill.CalculateValue()}";
            
            _unitEnergyText.text = Entity.CurrentEnergy.ToString();
            _unitShieldText.text = Entity.CurrentShield.ToString();
            _unitFieldText.text = Entity.CurrentField.ToString();
        }
    }
}