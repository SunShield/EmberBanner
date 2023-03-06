using ItemsManager.Databases;
using ItemsManager.Databases.Elements;
using ItemsManager.Editor.Tabs.Elements.Inspectors;
using ItemsManager.Editor.Tabs.Elements.Navigators;
using TabbedWindow.Tabs;
using TabbedWindow.Windows;
using UnityEngine;
using UnityEngine.UIElements;

namespace ItemsManager.Editor.Tabs
{
    public abstract class ManagerTab<TElement, TNavigatorElement, TDatabase, TGeneralDatabase, TInspector, TNavigator> 
        : AbstractTab<TGeneralDatabase>
        where TElement : class, IAbstractDatabaseElement
        where TNavigatorElement : AbstractNavigatorElement<TElement>, new()
        where TDatabase : IDatabase<TElement>
        where TGeneralDatabase : AbstractGeneralDatabase
        where TInspector : AbstractInspector<TElement, TDatabase>, new()
        where TNavigator : AbstractNavigator<TElement, TNavigatorElement, TDatabase>, new()
    {
        protected TDatabase Database { get; private set; }
        protected TNavigator Navigator { get; private set; }
        protected TInspector Inspector { get; private set; }

        protected TGeneralDatabase GeneralDatabase => TargetObject;
        
        protected ManagerTab(AbstractWindow window, string name, string assetLocation) : base(window, name, assetLocation)
        {
        }

        public void Prepare()
        {
            Database = GetDatabase();
            BuildDivider();
            Inspector.SetDatabase(Database);
            Inspector.Prepare(this);
            BuildNavigator();
            PostPrepare();
        }

        protected abstract TDatabase GetDatabase();

        private void BuildDivider()
        {
            var divider = new VisualElement();
            divider.style.backgroundColor = Color.black;
            divider.style.width = 2f;
            Insert(0, divider);
        }

        private void BuildNavigator()
        {
            Navigator = new();
            SetNavigatorEvents();
            Navigator.SetDatabase(Database);
            Insert(0, Navigator);
            Inspector.onElementUpdated += Navigator.OnElementUpdated;
            ConfigureNavigator(Navigator);
        }

        private void SetNavigatorEvents()
        {
            Navigator.onSelectedElementChanged += OnNavigatorSelectedItemChanged;
            Navigator.onNoElementsSelected += () =>
            {
                Inspector.style.visibility = Visibility.Hidden;
            };
        }

        protected virtual void ConfigureNavigator(TNavigator navigator) { }
        protected virtual void PostPrepare() { }

        private void OnNavigatorSelectedItemChanged(TElement element)
        {
            Inspector.style.visibility = element != null ? Visibility.Visible : Visibility.Hidden;
            if (element == null) return;
            
            Inspector.SetElement(element);
        }

        protected sealed override void BuildContent()
        {
            BuildInspector();
            BuildManagerTabContent();
        }

        private void BuildInspector()
        {
            Inspector = new();
            Inspector.visible = false;
            ContentContainer.Add(Inspector);
        }

        protected virtual void BuildManagerTabContent() { }
    }
}