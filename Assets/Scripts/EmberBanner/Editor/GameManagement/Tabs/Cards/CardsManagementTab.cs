using System;
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
        
        protected override void PostPrepare()
        {
            Navigator.onElementAdded += FireOnCardAddedEvent;
            Navigator.onElementRemoved += unit => FireOnCardRemovedEvent(unit.Name);
        }
        
        private void FireOnCardAddedEvent(string unitName) => onCardAdded?.Invoke(unitName);
        private void FireOnCardRemovedEvent(string unitName) => onCardRemoved?.Invoke(unitName);

        public event Action<string> onCardAdded;
        public event Action<string> onCardRemoved;
    }
}