using System.Collections.Generic;
using EmberBanner.Editor.GameManagement.Tabs.Cards;
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

            return new() { cardsTab };
        }
    }
}