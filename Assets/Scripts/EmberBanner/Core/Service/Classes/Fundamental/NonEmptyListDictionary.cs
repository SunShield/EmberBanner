using System.Collections.Generic;

namespace EmberBanner.Core.Service.Classes.Fundamental
{
    public class NonEmptyListDictionary<TKey, TValue>
    {
        private Dictionary<TKey, List<TValue>> _elements = new();

        public void Add(TKey key, TValue value)
        {
            if (!_elements.ContainsKey(key)) _elements.Add(key, new());
            _elements[key].Add(value);
        }

        public void Remove(TKey key, TValue value)
        {
            _elements[key].Remove(value);
            if (_elements[key].Count == 0) _elements.Remove(key);
        }

        public bool ContainsKey(TKey key) => _elements.ContainsKey(key);

        public void Clear() => _elements.Clear();

        public List<TValue> this[TKey key] => _elements[key];
    }
}