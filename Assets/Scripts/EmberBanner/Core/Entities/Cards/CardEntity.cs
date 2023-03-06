using EmberBanner.Core.Ingame.Units;
using EmberBanner.Core.Models.Cards;

namespace EmberBanner.Core.Ingame.Cards
{
    public class CardEntity : AbstractEntity<CardModel>
    {
        public UnitEntity Owner { get; private set; }

        public CardEntity(int id, CardModel model) : base(id, model)
        {
        }

        public void SetOwner(UnitEntity owner) => Owner = owner;
    }
}