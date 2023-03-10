using System.Collections.Generic;
using EmberBanner.Core.Entities.Impl.Units;
using EmberBanner.Core.Entities.Management.SaveLoad.Data.Impl.Cards;
using EmberBanner.Core.Enums.Actions;
using EmberBanner.Core.Enums.Battle.Targeting;
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
        {
            var result = new List<ActionModel>();
            foreach (var actionModel in Model.Actions)
            {
                if (actionModel.Type == ActionType.Aggression && actionModel.AggressionTargetType.AllowsEnemy() ||
                    actionModel.Type == ActionType.Defense && actionModel.DefenseTargetType.AllowsEnemy())
                    result.Add(actionModel);
            }

            return result;
        }
        
        public List<ActionModel> GetActionsTargetingAlly()
        {
            var result = new List<ActionModel>();
            foreach (var actionModel in Model.Actions)
            {
                if (actionModel.Type == ActionType.Defense && actionModel.DefenseTargetType.AllowsAlly() ||
                    actionModel.Type == ActionType.Support && actionModel.SupportTargetType.AllowsAlly())
                    result.Add(actionModel);
            }

            return result;
        }
        
        public List<ActionModel> GetActionsTargetingSelf()
        {
            var result = new List<ActionModel>();
            foreach (var actionModel in Model.Actions)
            {
                if (actionModel.Type == ActionType.Aggression && actionModel.AggressionTargetType.AllowsSelf() ||
                    actionModel.Type == ActionType.Defense && actionModel.DefenseTargetType.AllowsSelf() ||
                    actionModel.Type == ActionType.Support && actionModel.SupportTargetType.AllowsSelf())
                    result.Add(actionModel);
            }

            return result;
        }
    }
}