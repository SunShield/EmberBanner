using EmberBanner.Core.Models.Units;
using EmberBanner.Editor.GameManagement.Tabs.Units.Elements;
using EmberBanner.Unity.Data.ScriptableObjects.Databases;
using ItemsManager.Editor.Tabs;
using TabbedWindow.Windows;

namespace EmberBanner.Editor.GameManagement.Tabs.Units
{
    public class UnitsManagementTab : DictionaryDatabaseManagerTab<string, UnitModel, UnitNavigatorElement, UnitsDatabase, GeneralDatabase, UnitInspector, UnitNavigator>
    {
        public UnitsManagementTab(AbstractWindow window, string name, string assetLocation) : base(window, name, assetLocation)
        {
        }

        protected override UnitsDatabase GetDatabase() => GeneralDatabase.Units;
    }
}