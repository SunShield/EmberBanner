using OerGraph.Runtime.Unity.Data;
using UnityEditor;

namespace EmberBanner.Editor.GraphAssets
{
    public class CardGraphAssetDestroyer
    {
        private static CardGraphAssetDestroyer _instance;
        public static CardGraphAssetDestroyer I => _instance ??= new();

        public void DestroyCardAsset(string cardName)
        {
            var assetPath = GetProperAssetPath(cardName);
            var asset = AssetDatabase.LoadAssetAtPath<OerGraphAsset>(assetPath);
            if (asset == null) return;

            AssetDatabase.DeleteAsset(assetPath);
        }
        
        private string GetProperAssetPath(string cardName) => $"{CardGraphAssetBuilder.CardGraphsPath}/{cardName}.asset";
    }
}