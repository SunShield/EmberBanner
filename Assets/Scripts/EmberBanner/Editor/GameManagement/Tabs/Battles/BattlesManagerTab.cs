using EmberBanner.Core.Models.Battles;
using EmberBanner.Editor.GameManagement.Tabs.Battles.Elements;
using EmberBanner.Unity.Data.ScriptableObjects.Databases;
using ItemsManager.Editor.Tabs;
using TabbedWindow.Windows;

namespace EmberBanner.Editor.GameManagement.Tabs.Battles
{
    public class BattlesManagerTab : DictionaryDatabaseManagerTab<string, BattleModel, BattleNavigatorElement, BattlesDatabase, GeneralDatabase, BattleInspector, BattleNavigator>
    {
        public BattlesManagerTab(AbstractWindow window, string name, string assetLocation) : base(window, name, assetLocation)
        {
        }

        protected override BattlesDatabase GetDatabase() => GeneralDatabase.Battles;
    }
}