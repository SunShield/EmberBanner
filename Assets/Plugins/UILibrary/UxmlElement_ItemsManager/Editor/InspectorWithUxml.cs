using ItemsManager.Databases;
using ItemsManager.Databases.Elements;
using ItemsManager.Editor.Tabs.Elements.Inspectors;
using UILibrary.UxmlElement.Editor;
using UnityEngine.UIElements;

namespace NFate.Editor.EditorElements
{
    public abstract class InspectorWithUxml<TElement, TDatabase> : AbstractInspector<TElement, TDatabase>
        where TElement : IAbstractDatabaseElement
        where TDatabase : IDatabase<TElement>
    {
        private const string RootElementName = "Root";
        
        protected abstract string UxmlKey { get; }
        
        protected VisualElement Root { get; private set; }

        protected InspectorWithUxml() : base()
        {
            BuildGeometry();
        }

        private void BuildGeometry()
        {
            var visualTree = UxmlDatabase.Instance.Uxmls[UxmlKey];
            Root = visualTree.CloneTree().Q<VisualElement>(RootElementName);
            Add(Root);
        }
    }
}