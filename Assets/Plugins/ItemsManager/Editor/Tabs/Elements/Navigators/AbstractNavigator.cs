using System;
using System.Collections.Generic;
using ItemsManager.Databases;
using ItemsManager.Databases.Elements;
using TabbedWindow.Tabs;
using UnityEngine.UIElements;

namespace ItemsManager.Editor.Tabs.Elements.Navigators
{
    public abstract class AbstractNavigator<TElement, TNavigatorElement, TDatabase> : VisualElement
        where TElement : class, IAbstractDatabaseElement
        where TNavigatorElement : AbstractNavigatorElement<TElement>, new()
        where TDatabase : IDatabase<TElement>
    {
        protected const int NoElementSelected = -1;
        
        protected TDatabase Database { get; private set; }

        protected HashSet<string> ElementNames { get; private set; } = new();
        protected Dictionary<TElement, TNavigatorElement> NavigatorElementsMap { get; private set; } = new();
        protected List<TNavigatorElement> NavigatorElements { get; private set; } = new();
        protected int CurrentNavigatorElementIndex = NoElementSelected;
        protected TNavigatorElement CurrentNavigatorElement => CurrentNavigatorElementIndex < NavigatorElements.Count 
            ? NavigatorElements[CurrentNavigatorElementIndex]
            : null;
        protected TElement CurrentElement => CurrentNavigatorElement?.WrappedElement;
        public IAbstractTab Tab { get; private set; }

        public void SetDatabase(TDatabase database)
        {
            Database = database;
            var dbElements = Database.GetElementsForIteration();
            for (int i = 0; i < dbElements.Count; i++)
            {
                var element = dbElements[i];
                AddExistingElement(element);
            }
        }

        protected void AddExistingElement(TElement element)
        {
            ElementNames.Add(element.Name);
            CreateNavigatorElement(element);
            
            if (NavigatorElements.Count == 1) SelectElementInternal(NavigatorElementsMap[element]);
        }

        public void TryAddElement(string elementName)
        {
            if (ElementNames.Contains(elementName)) return;

            AddElement(elementName);
        }

        protected void AddElement(string elementName)
        {
            var element = ConstructElement(elementName);
            Database.AddElement(element);
            ElementNames.Add(element.Name);
            CreateNavigatorElement(element);
            Database.Update();

            if (Database.Count == 1) SelectElementInternal(NavigatorElementsMap[element]);
            onElementAdded?.Invoke(elementName);
        }

        protected abstract TElement ConstructElement(string name); 

        protected void CreateNavigatorElement(TElement element)
        {
            var navigatorElement = new TNavigatorElement();
            navigatorElement.BindElement(element);
            navigatorElement.onClick += (el) => SelectElementInternal(el as TNavigatorElement);
            NavigatorElements.Add(navigatorElement);
            PostCreateNavigatorElement(element, navigatorElement);
            NavigatorElementsMap.Add(element, navigatorElement);
        }

        /// <summary>
        /// Use this to add NavigatorElement to certain VisualElement etc
        /// </summary>
        /// <param name="element"></param>
        /// <param name="navigatorElement"></param>
        protected abstract void PostCreateNavigatorElement(TElement element, TNavigatorElement navigatorElement);

        public void RemoveElement(TNavigatorElement navigatorElement)
        {
            ElementNames.Remove(navigatorElement.WrappedElement.Name);
            Database.RemoveElement(navigatorElement.WrappedElement);
            Database.Update();
            NavigatorElementsMap.Remove(navigatorElement.WrappedElement);
            
            var index = GetNavigatorElementIndex(navigatorElement);
            NavigatorElements.RemoveAt(index);
            
            onElementRemoved?.Invoke(navigatorElement.WrappedElement);
            
            if (CurrentNavigatorElementIndex != index) return;
            if (NavigatorElements.Count == 0)
            {
                SetNoElementSelected();
                return;
            }
            
            SelectElementInternal(CurrentNavigatorElementIndex == NavigatorElements.Count
                ? NavigatorElements[CurrentNavigatorElementIndex - 1]
                : NavigatorElements[CurrentNavigatorElementIndex]);
        }

        protected void SelectElementInternal(TNavigatorElement navigatorElement)
        {
            if (CurrentNavigatorElementIndex != NoElementSelected)
                CurrentNavigatorElement?.SetSelected(false);
            
            var index = GetNavigatorElementIndex(navigatorElement);
            CurrentNavigatorElementIndex = index;
            CurrentNavigatorElement.SetSelected(true);
            
            onSelectedElementChanged?.Invoke(CurrentElement);
        }

        protected int GetNavigatorElementIndex(TNavigatorElement navigatorElement)
        {
            for (int i = 0; i < NavigatorElements.Count; i++)
            {
                if (navigatorElement != NavigatorElements[i]) continue;
                return i;
            }

            return NoElementSelected;
        }

        protected void SetNoElementSelected()
        {
            CurrentNavigatorElementIndex = NoElementSelected;
            onNoElementsSelected?.Invoke();
        }

        public void OnElementUpdated()
        {
            CurrentNavigatorElement.DoUpdateElement();
        }

        public event Action<TElement> onElementRemoved;
        public event Action<string> onElementAdded; 
        public event Action<TElement> onSelectedElementChanged;
        public event Action onNoElementsSelected;
    }
}