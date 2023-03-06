using ItemsManager.Databases;
using ItemsManager.Databases.Elements;
using ItemsManager.Editor.Tabs.Elements.Inspectors;
using ItemsManager.Editor.Tabs.Elements.Navigators;
using TabbedWindow.Windows;

namespace ItemsManager.Editor.Tabs
{
    public abstract class ListDatabaseManagerTab<TElement, TNavigatorElement, TDatabase, TGeneralDatabase, TInspector, TNavigator> 
        : ManagerTab<TElement, TNavigatorElement, TDatabase, TGeneralDatabase, TInspector, TNavigator>
        where TElement : class, IAbstractDatabaseElement
        where TNavigatorElement : AbstractNavigatorElement<TElement>, new()
        where TDatabase : IListDatabase<TElement>
        where TGeneralDatabase : AbstractGeneralDatabase
        where TInspector : AbstractInspector<TElement, TDatabase>, new()
        where TNavigator : ListNavigator<TElement, TNavigatorElement, TDatabase>, new()
    {
        protected ListDatabaseManagerTab(AbstractWindow window, string name, string assetLocation) : base(window, name, assetLocation)
        {
        }
    }
}