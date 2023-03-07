using System;
using EmberBanner.Core.Entities.Management.Factories;
using EmberBanner.Core.Models;

namespace EmberBanner.Core.Ingame.Management.Factories
{
    public abstract class EntityFactory<TEntity, TModel>
        where TEntity : AbstractEntity<TModel>
        where TModel : AbstractModel
    {
        public TEntity CreateEntity(TModel model)
        {
            var entity = Activator.CreateInstance(typeof(TEntity), EntityIdHolder.GetId, model) as TEntity;
            OnPostCreateEntity(entity, model);
            return entity;
        }

        protected virtual void OnPostCreateEntity(TEntity entity, TModel model) { }
    }
}