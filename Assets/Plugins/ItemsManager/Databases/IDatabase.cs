using System.Collections.Generic;
using ItemsManager.Databases.Elements;

namespace ItemsManager.Databases
{
    public interface IDatabase<TElement>
        where TElement : IAbstractDatabaseElement
    {
        int Count { get; }
        void Update();
        void AddElement(TElement element);
        void RemoveElement(TElement element);
        List<TElement> GetElementsForIteration();
    }
}