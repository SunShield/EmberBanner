using System.Collections.Generic;
using EmberBanner.Editor.GameManagement.Tabs.Battles;
using EmberBanner.Editor.GameManagement.Tabs.Cards;
using EmberBanner.Editor.GameManagement.Tabs.Units;
using TabbedWindow.Tabs;
using TabbedWindow.Windows;
using UnityEditor;
using UnityEngine;

namespace EmberBanner.Editor.GameManagement.Windows
{
    public class GameManagerWindow : AbstractWindow
    {
        [MenuItem("Game Management/Manager Window")]
        public static void Show()
        {
            var window = GetWindow<GameManagerWindow>();
            window.titleContent = new GUIContent("Manager Window");
        }
        
        protected override string Title { get; } = "Game Manager";
        
        protected override List<IAbstractTab> GetTabs()
        {
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

            return new() { cardsTab, unitsTab, battlesTab };
        }
    }
}