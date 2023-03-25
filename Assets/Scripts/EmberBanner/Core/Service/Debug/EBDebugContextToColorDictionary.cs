using System;
using Serializables;
using UnityEngine;

namespace EmberBanner.Core.Service.Debug
{
    [Serializable]
    public class EBDebugContextToColorDictionary : SerializableDictionary<EBDebugContext, Color>
    {
    }
}