using EmberBanner.Core.Enums.Battle;
using EmberBanner.Unity.Battle.Views.Impl.Cards;
using UnityEngine;

namespace EmberBanner.Unity.Battle.Systems.CardZonesSystem.Zones
{
    public class HandCardZone : AbstractCardZone
    {
        public override BattleCardZone Type { get; } = BattleCardZone.Hand;
        
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