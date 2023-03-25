using OerGraph.Runtime.Unity.Data;
using UnityEditor;
using UnityEngine;

namespace EmberBanner.Editor.GraphAssets
{
    public class CardGraphAssetBuilder
    {
        private static CardGraphAssetBuilder _instance;
        public static CardGraphAssetBuilder I => _instance ??= new();
        
        public const string CardGraphsPath = @"Assets/Data/Databases/CardGraphs";

        public OerGraphAsset BuildCardGraphAsset(string cardName)
        {
            var asset = ScriptableObject.CreateInstance<OerGraphAsset>();
            asset.Graphs = new();
            
            AssetDatabase.CreateAsset(asset, $"{CardGraphsPath}/{cardName}.asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            
            return asset;
        }
    }
}