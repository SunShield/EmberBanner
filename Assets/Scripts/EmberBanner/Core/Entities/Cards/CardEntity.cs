using EmberBanner.Core.Ingame.Units;
using EmberBanner.Core.Models.Cards;

namespace EmberBanner.Core.Ingame.Cards
{
    public class CardEntity : AbstractEntity<CardModel>
    {
        public UnitEntity Owner { get; private set; }
    }
}