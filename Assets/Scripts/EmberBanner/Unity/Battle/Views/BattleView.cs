using EmberBanner.Core.Ingame;
using EmberBanner.Core.Models;
using EmberBanner.Unity.Views;

namespace EmberBanner.Unity.Battle.Views
{
    public abstract class BattleView<TEntity, TModel> : AbstractView<TEntity, TModel>
        where TModel : AbstractModel
        where TEntity : AbstractEntity<TModel>
    {
    }
}