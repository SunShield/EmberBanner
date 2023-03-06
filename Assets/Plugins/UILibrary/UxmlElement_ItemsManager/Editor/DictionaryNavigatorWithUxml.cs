using System;
using ItemsManager.Databases;
using ItemsManager.Databases.Elements;
using ItemsManager.Editor.Tabs.Elements.Navigators;
using UILibrary.UxmlElement.Editor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace NFate.Editor.EditorElements
{
    public abstract class DictionaryNavigatorWithUxml<TKey, TElement, TNavigatorElement, TDatabase> : DictionaryNavigator<TKey, TElement, TNavigatorElement, TDatabase>
        where TElement :  class, IAbstractDatabaseElement
        where TNavigatorElement : AbstractNavigatorElement<TElement>, new()
        where TDatabase : IDictionaryDatabase<TKey, TElement>
    {
        private const string RootElementName = "Root";

        protected abstract string UxmlKey { get; }

        protected VisualElement Root { get; private set; }
        protected TextField NewElementNameField { get; private set; }
        protected Button AddButton { get; private set; }
        protected Button RemoveButton { get; private set; }
        protected ToolbarSearchField SearchField { get; private set; }
        protected ScrollView ScrollView { get; private set; }

        protected DictionaryNavigatorWithUxml() : base()
        {
            BuildGeometry();
            AddEvents();
        }

        private void BuildGeometry()
        {
            var visualTree = UxmlDatabase.Instance.Uxmls[UxmlKey];
            Root = visualTree.CloneTree().Q<VisualElement>(RootElementName);
            Root.style.width = 350f;
            Add(Root);

            NewElementNameField = Root.Q<TextField>("NewElementNameField");
            AddButton           = Root.Q<Button>("AddButton");
            RemoveButton        = Root.Q<Button>("RemoveButton");
            SearchField         = Root.Q<ToolbarSearchField>("SearchField");
            ScrollView          = Root.Q<ScrollView>("ElementsContainer");
        }
        
        private void AddEvents()
        {
            AddButton.clicked += () =>
            {
                var name = NewElementNameField.text;
                if(string.IsNullOrEmpty(name)) return;
                
                TryAddElement(name);
            };
            RemoveButton.clicked += () =>
            {
                if (CurrentElement == null) return;

                ScrollView.Remove(CurrentNavigatorElement);
                RemoveElement(CurrentNavigatorElement);
            };
            SearchField.RegisterValueChangedCallback(OnSearchFieldContentsChanged);
        }

        protected override TElement ConstructElement(string name)
            => Activator.CreateInstance(typeof(TElement), name) as TElement;

        protected override void PostCreateNavigatorElement(TElement element, TNavigatorElement navigatorElement)
        {
            ScrollView.Add(navigatorElement);
        }

        private void OnSearchFieldContentsChanged(ChangeEvent<string> ent)
        {
            foreach (var navigatorElement in NavigatorElements)
            {
                navigatorElement.style.display = navigatorElement.WrappedElement.Name.StartsWith(ent.newValue) ? DisplayStyle.Flex : DisplayStyle.None;
            }
        }
    }
}