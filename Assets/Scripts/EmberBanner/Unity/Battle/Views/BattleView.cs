using EmberBanner.Core.Ingame;
using EmberBanner.Core.Models;
using EmberBanner.Unity.Views;

namespace EmberBanner.Unity.Battle.Views
{
    /// <summary>
    /// Unlike other View, BattleViews are used to directly store and manipulate actual battle entities state
    ///
    /// This violates the OOP philosophies but simplifies coding base too much
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TModel"></typeparam>
    public abstract class BattleView<TEntity, TModel> : AbstractView<TEntity, TModel>
        where TModel : AbstractModel
        where TEntity : AbstractEntity<TModel>
    {
    }
}