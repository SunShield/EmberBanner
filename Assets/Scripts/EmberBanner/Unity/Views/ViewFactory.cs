using EmberBanner.Core.Ingame;
using EmberBanner.Core.Models;
using EmberBanner.Unity.Service;

namespace EmberBanner.Unity.Views
{
    public abstract class ViewFactory<TView, TEntity, TModel> : EBMonoBehaviour
        where TModel : AbstractModel
        where TEntity : AbstractEntity<TModel>
        where TView : AbstractView<TEntity, TModel>
    {
        public TView CreateView(TEntity entity)
        {
            var prefab = GetPrefab(entity);
            var view = Instantiate(prefab);
            PostCreateView(view);
            return view;
        }

        protected abstract TView GetPrefab(TEntity entity);
        protected virtual void PostCreateView(TView view) { }
    }
}