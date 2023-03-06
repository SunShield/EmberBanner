using System.Collections.Generic;
using ItemsManager.Databases.Elements;

namespace ItemsManager.Databases
{
    public interface IDictionaryDatabase<TKey, TElement> : IDatabase<TElement>
        where TElement : IAbstractDatabaseElement
    {
        IDictionary<TKey, TElement> Elements { get; }
    }
}