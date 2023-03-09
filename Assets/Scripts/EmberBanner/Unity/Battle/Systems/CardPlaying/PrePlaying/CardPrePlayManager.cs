using EmberBanner.Unity.Battle.Views.Impl.Cards;
using EmberBanner.Unity.Battle.Views.Impl.Units.Crystals;

namespace EmberBanner.Unity.Battle.Systems.CardPlaying.PrePlaying
{
    public class CardPrePlayManager
    {
        private static CardPrePlayManager _instance;
        public static CardPrePlayManager I => _instance ??= new();

        public void SetCardPrePlayed(BattleCardView card, BattleUnitCrystalView potentialOwner)
        {
            card.SetPrePlayed(potentialOwner);
        }

        public void SetCardPrePlayedWithTarget(BattleCardView card, BattleUnitCrystalView potentialOwner, BattleUnitCrystalView potentialTarget)
        {
           

            SetCardPrePlayed(card, potentialOwner);
            // add to matrix
        }

        public void UnsetCardPrePlayed(BattleCardView card)
        {
            card.UnsetPrePlayed();
        }
    }
}