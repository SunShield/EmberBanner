using OerGraph.Runtime.Core.Graphs.Tools.EditorBased;
using OerGraph.Runtime.Unity.Data;
using UnityEditor;

namespace EmberBanner.Editor.Graphs.Cards.Management
{
    public class OerCardGraphManager
    {
        private static OerCardGraphManager _instance;
        public static OerCardGraphManager I => _instance ??= new();

        public void CreateCardGraph(OerGraphAsset cardGraphAsset)
        {
            var graph = OerGraphCreator.CreateGraph("Card");
            var data = new OerGraphData()
            {
                Name = "Card",
                Key = "Card",
                Graph = graph,
                EditorData = new() { Nodes = new() }
            };
            cardGraphAsset.Graphs.Add(data.Name, data);
            EditorUtility.SetDirty(cardGraphAsset);
        }
        
        public void CreateActionGraph(OerGraphAsset cardGraphAsset, string actionName)
        {
            var graph = OerGraphCreator.CreateGraph("Action");
            var data = new OerGraphData()
            {
                Name = actionName,
                Key = actionName,
                Graph = graph,
                EditorData = new() { Nodes = new() }
            };
            cardGraphAsset.Graphs.Add(data.Name, data);
            EditorUtility.SetDirty(cardGraphAsset);
        }

        public void RemoveActionGraph(OerGraphAsset cardGraphAsset, string actionName)
        {
            cardGraphAsset.Graphs.Remove(actionName);
            EditorUtility.SetDirty(cardGraphAsset);
        }
    }
}