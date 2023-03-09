using EmberBanner.Core.Enums.Battle;
using EmberBanner.Core.Ingame.Impl.Battles;
using EmberBanner.Core.Models.Units.Crystals;
using EmberBanner.Unity.Battle.Systems.CardZonesSystem.Zones;
using EmberBanner.Unity.Battle.Views.Impl.Cards;
using TMPro;
using UnityEngine;

namespace EmberBanner.Unity.Battle.Views.Impl.Units.Crystals
{
    public class BattleUnitCrystalView : BattleView<BattleUnitCrystalEntity, UnitCrystalModel>
    {
        [SerializeField] private TextMeshPro _rollText;
        [SerializeField] private PlayCardZone _zone;
        
        public BattleUnitView OwnerView { get; private set; }
        
        public int? CurrentRoll => Entity.CurrentRoll;
        public UnitControllerType Controller => Entity.Owner.Controller;
        public BattleCardView Card => _zone.Cards[0];

        public void SetOwnerView(BattleUnitView ownerView) => OwnerView = ownerView;

        public void SetCard(BattleCardView card)
        {
            if (Card != null) UnsetCard();
            _zone.AddCard(card);
            card.SetCrystal(this);
        }

        public void UnsetCard()
        {
            if (Card == null) return;
            _zone.RemoveCard(Card);
            Card.SetCrystal(null);
        }

        public void Roll()
        {
            Entity.DoRoll();
            _rollText.text = CurrentRoll.ToString();
        }

        public void Clear()
        {
            _rollText.text = "X";
        }
    }
}