using EmberBanner.Core.Ingame;
using EmberBanner.Core.Models;
using EmberBanner.Unity.Views;

namespace EmberBanner.Unity.Battle.Views
{
    public abstract class BattleView<TModel, TEntity> : AbstractView<TModel, TEntity>
        where TModel : AbstractModel
        where TEntity : AbstractEntity<TModel>
    {
        public int Id => Entity.Id;
    }
}