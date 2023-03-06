using System.Collections.Generic;
using EmberBanner.Core.Ingame.Cards;
using EmberBanner.Core.Models.Units;

namespace EmberBanner.Core.Ingame.Units
{
    public class UnitEntity : AbstractEntity<UnitModel>
    {
        public List<CardEntity> Deck { get; private set; } = new();
    }
}