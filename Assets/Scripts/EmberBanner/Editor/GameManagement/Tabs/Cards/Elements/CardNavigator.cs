using EmberBanner.Core.Models.Cards;
using EmberBanner.Editor.GraphAssets;
using EmberBanner.Editor.Graphs.Cards.Management;
using EmberBanner.Unity.Data.ScriptableObjects.Databases;
using NFate.Editor.EditorElements;

namespace EmberBanner.Editor.GameManagement.Tabs.Cards.Elements
{
    public class CardNavigator : DictionaryNavigatorWithUxml<string, CardModel, CardNavigatorElement, CardsDatabase>
    {
        protected override string UxmlKey { get; } = "CardNavigator";

        protected override void PostConstructElement(CardModel element)
        {
            element.GraphAsset = CardGraphAssetLoader.I.GetOrCreateCardGraphAsset(element.Name);
            OerCardGraphManager.I.CreateCardGraph(element.GraphAsset);
        }

        protected override void PreRemoveElement(CardModel element)
        {
            CardGraphAssetDestroyer.I.DestroyCardAsset(element.Name);
        }
    }
}