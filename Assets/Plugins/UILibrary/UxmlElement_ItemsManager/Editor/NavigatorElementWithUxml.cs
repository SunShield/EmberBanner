using ItemsManager.Databases.Elements;
using ItemsManager.Editor.Tabs.Elements.Navigators;
using UILibrary.UxmlElement.Editor;
using UnityEngine;
using UnityEngine.UIElements;

namespace NFate.Editor.EditorElements
{
    public abstract class NavigatorElementWithUxml<TElement> : AbstractNavigatorElement<TElement>
        where TElement : IAbstractDatabaseElement
    {
        private const string RootElementName = "Root";
        protected virtual Color SelectedColor { get; } = new (0.4f, 0.7f, 0.9f, 1f);
        
        protected abstract string UxmlKey { get; }
        
        protected VisualElement Root { get; private set; }
        protected Color DefaultBgColor { get; private set; }

        protected NavigatorElementWithUxml() : base()
        {
            DefaultBgColor = style.backgroundColor.value;
            BuildGeometry();
        }

        private void BuildGeometry()
        {
            var treeAsset = UxmlDatabase.Instance.Uxmls[UxmlKey];
            Root = treeAsset.CloneTree().Q<VisualElement>(RootElementName);
            Add(Root);
        }
        
        public override void SetSelected(bool selected)
        {
            Root.style.backgroundColor = !selected ? DefaultBgColor : SelectedColor;
        }
    }
}