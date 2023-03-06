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
    public abstract class AbstractView<TModel, TEntity> : EBMonoBehaviour
        where TModel : AbstractModel
        where TEntity : AbstractEntity<TModel>
    {
        public int ViewId { get; private set; }
        public TEntity Entity { get; set; }
        public TModel Model => Entity.Model;
    }
}