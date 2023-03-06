using ItemsManager.Databases;
using ItemsManager.Databases.Elements;

namespace ItemsManager.Editor.Tabs.Elements.Navigators
{
    public abstract class DictionaryNavigator<TKey, TElement, TNavigatorElement, TDatabase> 
        : AbstractNavigator<TElement, TNavigatorElement, TDatabase>
        where TElement :  class, IAbstractDatabaseElement
        where TNavigatorElement : AbstractNavigatorElement<TElement>, new()
        where TDatabase : IDictionaryDatabase<TKey, TElement>
    {
        public TKey CurrentElementKey { get; set; }
    }
}