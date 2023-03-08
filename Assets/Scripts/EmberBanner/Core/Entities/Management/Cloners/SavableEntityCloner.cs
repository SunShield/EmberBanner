using EmberBanner.Core.Entities.Management.SaveLoad.Data;
using EmberBanner.Core.Ingame.Management.SaveLoad;
using EmberBanner.Core.Models;

namespace EmberBanner.Core.Ingame.Management.Cloners
{
    public class SavableEntityCloner<TEntity, TModel, TSaveData> : EntityCloner<TEntity, TModel>
        where TEntity : SavableEntity<TModel, TSaveData>
        where TModel : AbstractModel
        where TSaveData : AbstractEntitySaveData, new()
    {
        protected sealed override void ProcessClonedEntity(TEntity originalEntity, TEntity entityClone)
        {
            entityClone.PostLoad(originalEntity.GenerateSaveData());
        }

        protected virtual void ProcessEntityPostInitialize(TEntity originalEntity, TEntity entityClone) { }
    }
}