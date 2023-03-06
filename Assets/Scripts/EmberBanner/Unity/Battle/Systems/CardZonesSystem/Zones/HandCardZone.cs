using EmberBanner.Unity.Battle.Views.Cards;
using UnityEngine;

namespace EmberBanner.Unity.Battle.Systems.CardZonesSystem.Zones
{
    public class HandCardZone : AbstractCardZone
    {
        protected override void DoAddCard(BattleCardView card)
        {
            card.gameObject.SetActive(false);
            card.Tran.SetParent(CardsOrigin);
            card.Tran.localPosition = Vector3.zero;
            ChangeCardsOrder();
        }

        protected override void DoRemoveCard(BattleCardView card)
        {
            ChangeCardsOrder();
        }

        private void ChangeCardsOrder()
        {
            
        }
    }
}