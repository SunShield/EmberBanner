using EmberBanner.Core.Models.Cards;
using EmberBanner.Unity.Data.ScriptableObjects.Databases;
using NFate.Editor.EditorElements;

namespace EmberBanner.Editor.GameManagement.Tabs.Cards.Elements
{
    public class CardNavigator : DictionaryNavigatorWithUxml<string, CardModel, CardNavigatorElement, CardsDatabase>
    {
        protected override string UxmlKey { get; } = "CardNavigator";
    }
}