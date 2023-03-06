using System.Collections.Generic;
using ItemsManager.Databases.Elements;

namespace ItemsManager.Databases
{
    public interface IListDatabase<TElement> : IDatabase<TElement>
        where TElement : IAbstractDatabaseElement
    {
        List<TElement> Elements { get; }
    }
}