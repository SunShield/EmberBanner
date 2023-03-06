using System.Collections.Generic;
using EmberBanner.Core.Ingame.Cards;
using EmberBanner.Core.Ingame.Units.Crystals;
using EmberBanner.Core.Models.Units;

namespace EmberBanner.Core.Ingame.Units
{
    public class UnitEntity : AbstractEntity<UnitModel>
    {
        public List<UnitCrystalEntity> Crystals { get; private set; } = new();
        public List<CardEntity> Deck { get; private set; } = new();

        public UnitEntity(int id, UnitModel model) : base(id, model)
        {
        }
    }
}