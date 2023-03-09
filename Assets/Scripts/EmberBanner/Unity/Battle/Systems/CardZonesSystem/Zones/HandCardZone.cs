using EmberBanner.Core.Enums.Battle;
using EmberBanner.Unity.Battle.Views.Impl.Cards;
using UnityEngine;

namespace EmberBanner.Unity.Battle.Systems.CardZonesSystem.Zones
{
    public class HandCardZone : AbstractCardZone
    {
        private static float CardWidth = 2.3f;
        
        public override BattleCardZone Type { get; } = BattleCardZone.Hand;
        
        protected override void DoAddCard(BattleCardView card)
        {
            card.gameObject.SetActive(true);
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
            for (int i = 0; i < Cards.Count; i++)
            {
                Cards[i].Tran.localPosition = new Vector3(-(Cards.Count / 2f) * CardWidth + CardWidth / 2 + CardWidth * i, 0f, 0f);
            }
        }
    }
}