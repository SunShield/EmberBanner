using ItemsManager.Databases;
using ItemsManager.Databases.Elements;

namespace ItemsManager.Editor.Tabs.Elements.Navigators
{
    public abstract class ListNavigator<TElement, TNavigatorElement, TDatabase> : AbstractNavigator<TElement, TNavigatorElement, TDatabase>
        where TElement : class, IAbstractDatabaseElement
        where TNavigatorElement : AbstractNavigatorElement<TElement>, new()
        where TDatabase : IListDatabase<TElement>
    {
    }
}