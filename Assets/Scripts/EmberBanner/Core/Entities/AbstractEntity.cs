using EmberBanner.Core.Models;

namespace EmberBanner.Core.Ingame
{
    /// <summary>
    /// Ingame entity is a certain gameplay element based on an existing model
    ///
    /// Ingame entities are generated during game process. Some of them are saved during gameplay
    /// and loaded on app start. Other (like enemy cards) are temporary
    /// All entities share the same unique Id
    public abstract class AbstractEntity<TModel>
        where TModel : AbstractModel
    {
        public int Id { get; private set; }
        public TModel Model { get; private set; }

        public AbstractEntity(int id, TModel model)
        {
            Id = id;
            Model = model;
        }
    }
}