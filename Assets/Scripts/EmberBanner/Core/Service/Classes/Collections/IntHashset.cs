using System;
using Serializables;

namespace EmberBanner.Core.Service.Classes.Collections
{
    [Serializable]
    public class IntHashset : SerializableDictionary<int, bool>
    {
    }
}