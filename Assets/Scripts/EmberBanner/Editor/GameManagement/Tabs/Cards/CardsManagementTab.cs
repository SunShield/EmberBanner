using EmberBanner.Core.Models.Cards;
using EmberBanner.Editor.GameManagement.Tabs.Cards.Elements;
using EmberBanner.Unity.Data.ScriptableObjects.Databases;
using ItemsManager.Editor.Tabs;
using TabbedWindow.Windows;

namespace EmberBanner.Editor.GameManagement.Tabs.Cards
{
    public class CardsManagementTab : DictionaryDatabaseManagerTab<string, CardModel, CardNavigatorElement, CardsDatabase, GeneralDatabase, CardInspector, CardNavigator>
    {
        public CardsManagementTab(AbstractWindow window, string name, string assetLocation) : base(window, name, assetLocation)
        {
        }

        protected override CardsDatabase GetDatabase() => GeneralDatabase.Cards;
    }
}