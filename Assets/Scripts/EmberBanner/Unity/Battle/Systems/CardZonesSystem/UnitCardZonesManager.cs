using System.Collections.Generic;
using EmberBanner.Core.Ingame.Impl.Battles;
using EmberBanner.Unity.Battle.Systems.CardZonesSystem.Zones;
using EmberBanner.Unity.Battle.Views.Impl.Cards;
using EmberBanner.Unity.Battle.Views.Impl.Units;
using EmberBanner.Unity.Battle.Views.Impl.Units.Crystals;
using EmberBanner.Unity.Service;
using UnityEngine;
using BattleUnitCrystalView = EmberBanner.Unity.Battle.Views.Impl.Units.Crystals.BattleUnitCrystalView;

namespace EmberBanner.Unity.Battle.Systems.CardZonesSystem
{
    public class UnitCardZonesManager : EBMonoBehaviour
    {
        [SerializeField] private LibraryCardZone _library;
        [SerializeField] private HandCardZone _hand;
        [SerializeField] private GraveyardCardZone _graveyard;

        private BattleUnitView _owner;
        public int Draw     => _owner.Entity.Draw.CalculateValue();
        public int HandSize => _owner.Entity.HandSize.CalculateValue();

        public void Initialize(BattleUnitView owner)
        {
            _owner = owner;
            gameObject.name = $"CardZones [{_owner.Entity.Id}, {_owner.Entity.Model.Name}]";
        }

        public void AddCards(List<BattleCardView> cards)
        {
            foreach (var card in cards)
            {
                _library.AddCard(card);
            }
            _library.Shuffle();
        }

        public void DrawCardsAtBattleStart() => DrawCards(HandSize);
        public void DrawCardsAtTurnStart() => DrawCards(Draw);

        public void DrawCards(int cardsAmount)
        {
            var cardsToDraw = Mathf.Min(cardsAmount, _library.Count + _graveyard.Count);
            var drawnCardsCounter = 0;
            while (drawnCardsCounter < cardsToDraw && _hand.Count < HandSize)
            {
                DrawCard();
            }
        }

        public void DrawCard()
        {
            if (_library.TopDeck == null) ShuffleGraveyardInLibrary();
            if (_library.TopDeck == null) return; // every card is in hand
            
            var cardToDraw = _library.TopDeck;
            _library.RemoveCard(cardToDraw);
            _hand.AddCard(cardToDraw);
        }

        private void ShuffleGraveyardInLibrary()
        {
            foreach (var card in _graveyard.Cards)
            {
                _graveyard.RemoveCard(card);
                _library.AddCard(card);
            }
        }

        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }
        
        public void SetCardPrePlayed(BattleCardView card, BattleUnitCrystalView crystal)
        {
            _hand.RemoveCard(card);
            crystal.Zone.AddCard(card);
        }

        public void UnsetCardPrePlayed(BattleCardView card, BattleUnitCrystalView crystal)
        {
            crystal.Zone.RemoveCard(card);
            _hand.AddCard(card);
        }
    }
}