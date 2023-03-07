using EmberBanner.Core.Entities.Impl.Cards;
using EmberBanner.Core.Enums.Battle;
using EmberBanner.Core.Models.Cards;

namespace EmberBanner.Unity.Battle.Views.Cards
{
    public class BattleCardView : BattleView<CardModel, CardEntity>
    {
        public BattleCardZone Zone { get; private set; }

        public void LeaveZone()
        {
            
        }

        public void EnterZone()
        {
            
        }
    }
}