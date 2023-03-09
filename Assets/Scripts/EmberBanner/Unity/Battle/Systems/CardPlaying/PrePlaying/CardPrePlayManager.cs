using EmberBanner.Unity.Battle.Views.Impl.Cards;
using EmberBanner.Unity.Battle.Views.Impl.Units.Crystals;

namespace EmberBanner.Unity.Battle.Systems.CardPlaying.PrePlaying
{
    public class CardPrePlayManager
    {
        private static CardPrePlayManager _instance;
        public static CardPrePlayManager I => _instance ??= new();

        public void TryAddCardToCardTargetMatrix(BattleCardView card, BattleUnitCrystalView target)
        {
            var cardOwner = card.Crystal.OwnerView;
            if (!cardOwner.CanPlayCard(card)) return;
            cardOwner.PayCard(card);
        }

        public void ReturnCardFromCardTargetMatrix(BattleCardView card)
        {
            var cardOwner = card.Crystal.OwnerView;
            cardOwner.UnpayCard(card);
        }
    }
}