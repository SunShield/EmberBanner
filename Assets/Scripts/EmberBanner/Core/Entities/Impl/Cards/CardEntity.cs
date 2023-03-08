using System.Collections.Generic;
using EmberBanner.Core.Entities.Impl.Units;
using EmberBanner.Core.Entities.Management.SaveLoad.Data.Impl.Cards;
using EmberBanner.Core.Ingame.Management.SaveLoad;
using EmberBanner.Core.Models.Cards;
using Plugins.ComplexValue;

namespace EmberBanner.Core.Entities.Impl.Cards
{
    public class CardEntity : SavableEntity<CardModel, CardSaveData>
    {
        public UnitEntity Owner { get; private set; }
        public ComplexValue Cost { get; private set; }
        public List<CardActionEntity> Actions { get; private set; } = new();

        public CardEntity(int id, CardModel model) : base(id, model)
        {
            Cost = new(true, model.Cost);
        }

        public void SetOwner(UnitEntity owner) => Owner = owner;

        protected override CardSaveData GenerateSaveDataInternal(CardSaveData saveData)
        {
            return saveData;
        }
    }
}