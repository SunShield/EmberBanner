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

        public void OnUnitAdded(string unitName)
        {
            Inspector.Update();
        }
        
        public void OnUnitRemoved(string unitName)
        {
            foreach (var battleModel in Database.Elements.Values)
            {
                foreach (var determinedEnemy in battleModel.DeterminedEnemies)
                {
                    if (determinedEnemy.UnitName != unitName) continue;
                    determinedEnemy.UnitName = "";
                }
            }
            
            Inspector.Update();
        }
    }
}