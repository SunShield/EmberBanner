using System;
using ItemsManager.Databases.Elements;
using UnityEngine.UIElements;

namespace ItemsManager.Editor.Tabs.Elements.Navigators
{
    /// <summary>
    /// One item inside the navigator, actually it's view in list
    /// </summary>
    public abstract class AbstractNavigatorElement<TElement> : VisualElement
        where TElement : IAbstractDatabaseElement
    {
        public TElement WrappedElement { get; private set; }

        protected AbstractNavigatorElement()
        {
            this.AddManipulator(new Clickable(() => onClick?.Invoke(this)));
        }
        
        public void BindElement(TElement element)
        {
            WrappedElement = element;
            DoBindElement();
        }

        protected abstract void DoBindElement();
        public virtual void DoUpdateElement() { }

        public virtual void SetSelected(bool selected) { }

        public event Action<AbstractNavigatorElement<TElement>> onClick;
    }
}