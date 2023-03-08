using EmberBanner.Core.Entities.Management.SaveLoad.Data;
using EmberBanner.Core.Models;

namespace EmberBanner.Core.Ingame.Management.SaveLoad
{
    public abstract class SavableEntity<TModel, TSaveData> : AbstractEntity<TModel>
        where TModel : AbstractModel
        where TSaveData : AbstractEntitySaveData, new()
    {
        protected SavableEntity(int id, TModel model) : base(id, model)
        {
        }

        public void Initialize(TSaveData saveData)
        {
            PostLoadInternal(saveData);
        }

        /// <summary>
        /// This method applies all stuff from 
        /// </summary>
        /// <param name="saveData"></param>
        protected virtual void PostLoadInternal(TSaveData saveData) { }

        public TSaveData GenerateSaveData()
        {
            var data = new TSaveData()
            {
                Id = Id,
                ModelName = Model.Name
            };
            return GenerateSaveDataInternal(data);
        }

        /// <summary>
        /// Generates data for saving entity
        /// </summary>
        /// <returns></returns>
        protected abstract TSaveData GenerateSaveDataInternal(TSaveData saveData);
    }
}