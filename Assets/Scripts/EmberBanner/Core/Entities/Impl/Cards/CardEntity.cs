using System.Collections.Generic;
using EmberBanner.Core.Entities.Impl.Units;
using EmberBanner.Core.Entities.Management.Factories.Impl.Cards;
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
            
            foreach (var actionModel in model.Actions)
            {
                var actionEntity = CardActionEntityFactory.I.CreateEntity(actionModel);
                Actions.Add(actionEntity);
            }
        }

        public void SetOwner(UnitEntity owner) => Owner = owner;

        public override CardSaveData GenerateSaveDataInternal(CardSaveData saveData)
        {
            return saveData;
        }
    }
}