using System;
using EmberBanner.Unity.Battle.Views.Impl.Cards;
using EmberBanner.Unity.Battle.Views.Impl.Units.Crystals;

namespace EmberBanner.Unity.Battle.Systems.CardPlaying.TurnPlanning
{
    public class CardPrePlayManager
    {
        private static CardPrePlayManager _instance;
        public static CardPrePlayManager I => _instance ??= new();

        private void SetCardPrePlayed(BattleCardView card, BattleUnitCrystalView owner)
        {
            card.SetPrePlayed(owner);
            onCardSetPrePlay?.Invoke(owner);
        }

        public void SetCardPrePlayedWithTarget(BattleCardView card, BattleUnitCrystalView initiator, BattleUnitCrystalView target, bool defaultAttack = false)
        {
            SetCardPrePlayed(card, initiator);
            CardTargetsMatrix.I.AddAttack(initiator, target, defaultAttack);
        }

        public void UnsetCardPrePlayed(BattleCardView card)
        {
            onCardUnsetPrePlay?.Invoke(card.Crystal);
            if (CardTargetsMatrix.I.CrystalHasAttack(card.Crystal))
                CardTargetsMatrix.I.RemoveAttack(card.Crystal);
            card.UnsetPrePlayed();
        }

        public event Action<BattleUnitCrystalView> onCardSetPrePlay;
        public event Action<BattleUnitCrystalView> onCardUnsetPrePlay;
    }
}