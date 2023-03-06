using ItemsManager.Databases;
using ItemsManager.Databases.Elements;
using ItemsManager.Editor.Tabs.Elements.Inspectors;
using ItemsManager.Editor.Tabs.Elements.Navigators;
using TabbedWindow.Windows;

namespace ItemsManager.Editor.Tabs
{
    public abstract class DictionaryDatabaseManagerTab<TKey, TElement, TNavigatorElement, TDatabase, TGeneralDatabase, TInspector, TNavigator> 
        : ManagerTab<TElement, TNavigatorElement, TDatabase, TGeneralDatabase, TInspector, TNavigator>
        where TElement : class, IAbstractDatabaseElement
        where TNavigatorElement : AbstractNavigatorElement<TElement>, new()
        where TDatabase : IDictionaryDatabase<TKey, TElement>
        where TGeneralDatabase : AbstractGeneralDatabase
        where TInspector : AbstractInspector<TElement, TDatabase>, new()
        where TNavigator : DictionaryNavigator<TKey, TElement, TNavigatorElement, TDatabase>, new()
    {
        protected DictionaryDatabaseManagerTab(AbstractWindow window, string name, string assetLocation) : base(window, name, assetLocation)
        {
        }
    }
}