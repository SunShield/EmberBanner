using System.Collections.Generic;
using System.Linq;
using EmberBanner.Core.Entities.Impl.Units;
using EmberBanner.Core.Entities.Management.SaveLoad.Data.Impl.Cards;
using EmberBanner.Core.Ingame.Management.SaveLoad;
using EmberBanner.Core.Models.Actions;
using EmberBanner.Core.Models.Cards;
using EmberBanner.Core.Service.Extensions.Targeting;
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

        public List<ActionModel> GetActionsTargetingEnemy()
            => Model.Actions.Where(actionModel => actionModel.TargetType.AllowsEnemy()).ToList();
        
        public List<ActionModel> GetActionsTargetingAlly()
            => Model.Actions.Where(actionModel => actionModel.TargetType.AllowsAlly()).ToList();
        
        public List<ActionModel> GetActionsTargetingSelf()
            => Model.Actions.Where(actionModel => actionModel.TargetType.AllowsSelf()).ToList();
    }
}