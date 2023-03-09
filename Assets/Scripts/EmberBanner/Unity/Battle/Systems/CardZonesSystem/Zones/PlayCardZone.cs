using EmberBanner.Core.Enums.Battle;
using EmberBanner.Unity.Battle.Views.Impl.Cards;

namespace EmberBanner.Unity.Battle.Systems.CardZonesSystem.Zones
{
    public class PlayCardZone : AbstractCardZone
    {
        public override BattleCardZone Type { get; } = BattleCardZone.Play;

        protected override void DoAddCard(BattleCardView card)
        {
            card.Tran.parent = Tran;
            card.gameObject.SetActive(false);
        }

        protected override void DoRemoveCard(BattleCardView card)
        {
            card.gameObject.SetActive(true);
        }
    }
}