using EmberBanner.Unity.Battle.Views.Impl.Cards;
using EmberBanner.Unity.Battle.Views.Impl.Units.Crystals;

namespace EmberBanner.Unity.Battle.Systems.CardPlaying.PrePlaying
{
    public class CardPrePlayManager
    {
        private static CardPrePlayManager _instance;
        public static CardPrePlayManager I => _instance ??= new();

        public void SetCardPrePlayed(BattleCardView card, BattleUnitCrystalView owner)
        {
            card.SetPrePlayed(owner);
        }

        public void SetCardPrePlayedWithTarget(BattleCardView card, BattleUnitCrystalView initiator, BattleUnitCrystalView target, bool defaultAttack = false)
        {
            SetCardPrePlayed(card, initiator);
            CardTargetsMatrix.I.AddAttack(initiator, target, defaultAttack);
        }

        public void UnsetCardPrePlayed(BattleCardView card)
        {
            if (CardTargetsMatrix.I.HasAttack(card.Crystal))
                CardTargetsMatrix.I.RemoveAttack(card.Crystal);
            card.UnsetPrePlayed();
        }
    }
}