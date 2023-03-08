using EmberBanner.Core.Ingame;
using EmberBanner.Core.Models;
using EmberBanner.Unity.Service;

namespace EmberBanner.Unity.Views
{
    /// <summary>
    /// Views are classes capable of drawing Entities in Unity
    ///
    /// Entities can have multiple views (like, InventoryEntity, BattleEntity etc)
    /// </summary>
    public abstract class AbstractView<TEntity, TModel> : EBMonoBehaviour
        where TModel : AbstractModel
        where TEntity : AbstractEntity<TModel>
    {
        public TEntity Entity { get; set; }
        public int Id => Entity.Id;
        public TModel Model => Entity.Model;

        public void Initialize(TEntity entity)
        {
            Entity = entity;
            PostInitialize();
        }

        protected virtual void PostInitialize() { }
    }
}