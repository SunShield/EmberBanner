using System;
using EmberBanner.Core.Entities.Management.Factories;
using EmberBanner.Core.Models;

namespace EmberBanner.Core.Ingame.Management.Factories
{
    public abstract class EntityFactory<TEntity, TModel>
        where TEntity : AbstractEntity<TModel>
        where TModel : AbstractModel
    {
        /// <summary>
        /// Temp ids are always negative
        /// </summary>
        private static int _tempId = 0;
        private static int GetTempId => --_tempId;
        
        protected bool NextEntityIsTemporary { get; private set; }
        
        public TEntity CreateEntity(TModel model, bool isTemporaryEntity = false)
        {
            NextEntityIsTemporary = isTemporaryEntity;
            var entity = Activator.CreateInstance(typeof(TEntity), GetId(isTemporaryEntity), model) as TEntity;
            OnPostCreateEntity(entity, model);
            return entity;
        }

        private int GetId(bool temporaryEntity) => !temporaryEntity ? EntityIdHolder.GetId : GetTempId;

        private void PostCreateEntity(TEntity entity, TModel model)
        {
            OnPostCreateEntity(entity, model);
            NextEntityIsTemporary = false;
        }
        
        protected virtual void OnPostCreateEntity(TEntity entity, TModel model) { }
    }
}