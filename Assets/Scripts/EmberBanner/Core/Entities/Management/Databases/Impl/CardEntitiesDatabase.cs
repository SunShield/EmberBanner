using EmberBanner.Core.Entities.Impl.Cards;
using EmberBanner.Core.Entities.Management.SaveLoad.Data.Impl.Cards;
using EmberBanner.Core.Models.Cards;
using EmberBanner.Unity.Data;

namespace EmberBanner.Core.Entities.Management.Databases.Impl
{
    public class CardEntitiesDatabase : EntityDatabase<CardEntity, CardModel, CardSaveData>
    {
        protected override CardModel GetModel(string name) => DataHolder.I.Databases.Cards.Elements[name];
    }
}