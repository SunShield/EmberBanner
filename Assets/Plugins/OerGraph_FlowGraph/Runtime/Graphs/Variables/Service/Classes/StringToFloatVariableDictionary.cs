using System;
using OerGraph_FlowGraph.Runtime.Graphs.Variables.Impl;
using SpecialSerializables;

namespace OerGraph_FlowGraph.Runtime.Graphs.Variables.Service.Classes
{
    [Serializable]
    public class StringToFloatVariableDictionary : SpecialSerializableDictionary<string, OerGraphFloatVariable>
    {
    }
}