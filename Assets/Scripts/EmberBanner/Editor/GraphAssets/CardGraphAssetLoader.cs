using OerGraph.Runtime.Unity.Data;
using UnityEditor;

namespace EmberBanner.Editor.GraphAssets
{
    public class CardGraphAssetLoader
    {
        private static CardGraphAssetLoader _instance;
        public static CardGraphAssetLoader I => _instance ??= new();

        public OerGraphAsset GetOrCreateCardGraphAsset(string cardName)
        {
            var assetPath = GetProperAssetPath(cardName);
            var asset = AssetDatabase.LoadAssetAtPath<OerGraphAsset>(assetPath);
            if (asset == null)
                asset = CardGraphAssetBuilder.I.BuildCardGraphAsset(cardName);
            return asset;
        }
        
        private string GetProperAssetPath(string cardName) => $"{CardGraphAssetBuilder.CardGraphsPath}/{cardName}.asset";
    }
}