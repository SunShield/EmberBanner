using System;
using EmberBanner.Core.Models;

namespace EmberBanner.Core.Ingame.Management.Cloners
{
    public abstract class EntityCloner<TEntity, TModel>
        where TEntity : AbstractEntity<TModel>
        where TModel : AbstractModel
    {
        public TEntity Clone(TEntity originalEntity)
        {
            var entityClone = Activator.CreateInstance(typeof(TEntity), originalEntity.Id, originalEntity.Model) as TEntity;
            ProcessClonedEntity(originalEntity, entityClone);
            return entityClone;
        }

        protected virtual void ProcessClonedEntity(TEntity originalEntity, TEntity entityClone) { }
    }
}