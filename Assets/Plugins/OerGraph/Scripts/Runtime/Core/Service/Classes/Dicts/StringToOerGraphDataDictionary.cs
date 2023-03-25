using System;
using OerGraph.Runtime.Unity.Data;
using SpecialSerializables;

namespace OerGraph.Runtime.Core.Service.Classes.Dicts
{
    [Serializable]
    public class StringToOerGraphDataDictionary : SpecialSerializableDictionary<string, OerGraphData> { }
}