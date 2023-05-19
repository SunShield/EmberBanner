using System;
using ItemsManager.Databases;
using ItemsManager.Databases.Elements;
using TabbedWindow.Tabs;
using TabbedWindow.Windows;
using UnityEngine.UIElements;

namespace ItemsManager.Editor.Tabs.Elements.Inspectors
{
    public abstract class AbstractInspector<TElement, TDatabase> : VisualElement
        where TElement : IAbstractDatabaseElement
        where TDatabase : IDatabase<TElement>
    {
        protected TElement InspectedElement { get; private set; }
        protected TDatabase Database { get; private set; }
        public IAbstractTab Tab { get; private set; }

        public void Prepare(IAbstractTab tab)
        {
            Tab = tab;
            PostPrepare();
        }

        protected abstract void PostPrepare();
        
        public void SetDatabase(TDatabase database) => Database = database;

        public void SetElement(TElement element)
        {
            InspectedElement = element;
            OnElementSet();
        }

        protected void PostElementUpdate()
        {
            Database.Update();
            onElementUpdated?.Invoke();
        }

        protected abstract void OnElementSet();

        protected void UpdateDatabase() => Database.Update();

        public event Action onElementUpdated;
    }
}