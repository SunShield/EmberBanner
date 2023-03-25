using System;
using OerGraph_FlowGraph.Runtime.Graphs.Nodes;
using SpecialSerializables;

namespace OerGraph_FlowGraph.Runtime.Service.Classes
{
    [Serializable]
    public class StringToFlowNodeDictionary : SpecialSerializableDictionary<string, OerFlowNode>
    {
        
    }
}