using System.Collections.Generic;
using EmberBanner.Editor.GameManagement.Tabs.Battles;
using EmberBanner.Editor.GameManagement.Tabs.Cards;
using EmberBanner.Editor.GameManagement.Tabs.General;
using EmberBanner.Editor.GameManagement.Tabs.Units;
using OerGraph.Editor.Configuration;
using TabbedWindow.Tabs;
using TabbedWindow.Windows;
using UnityEditor;
using UnityEngine;

namespace EmberBanner.Editor.GameManagement.Windows
{
    public class GameManagerWindow : AbstractWindow
    {
        public static GameManagerWindow EI { get; private set; }
        
        [MenuItem("Game Management/Manager Window")]
        public static void Show()
        {
            EI = GetWindow<GameManagerWindow>();
            EI.titleContent = new GUIContent("Manager Window");
        }
        
        protected override string Title { get; } = "Game Manager";
        
        protected override List<IAbstractTab> GetTabs()
        {
            ApplyConfiguration();

            var gameDataTab = new GameDataTab(this, "Game Data", @"Assets/Data/GameData.asset");
            
            var cardsTab = new CardsManagementTab(this, "Cards", @"Assets/Data/Databases/GeneralDatabase.asset");
            cardsTab.Prepare();

            var unitsTab = new UnitsManagementTab(this, "Units", @"Assets/Data/Databases/GeneralDatabase.asset");
            unitsTab.Prepare();
            
            var battlesTab = new BattlesManagerTab(this, "Battles", @"Assets/Data/Databases/GeneralDatabase.asset");
            battlesTab.Prepare();

            cardsTab.onCardAdded += unitsTab.OnCardAdded;
            cardsTab.onCardRemoved += unitsTab.OnCardRemoved;
            
            unitsTab.onUnitAdded += battlesTab.OnUnitAdded;
            unitsTab.onUnitRemoved += battlesTab.OnUnitRemoved;

            return new() { gameDataTab, cardsTab, unitsTab, battlesTab };
        }

        private void ApplyConfiguration()
        {
            var configurator = (OerConfigurator)Resources.Load("OerConfigurator");
            configurator.ApplyConfiguration();
        }
    }
}