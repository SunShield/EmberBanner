using EmberBanner.Unity.Battle.Views.Impl.Cards;
using UnityEngine;

namespace EmberBanner.Unity.Battle.Systems.CardZonesSystem.Zones
{
    public class GraveyardCardZone : AbstractCardZone
    {
        protected override void DoAddCard(BattleCardView card)
        {
            card.gameObject.SetActive(false);
            card.Tran.SetParent(CardsOrigin);
            card.Tran.localPosition = Vector3.zero;
        }
    }
}