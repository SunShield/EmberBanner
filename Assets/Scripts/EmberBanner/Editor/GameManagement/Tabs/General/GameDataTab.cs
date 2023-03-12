using EmberBanner.Unity.Data.ScriptableObjects;
using TabbedWindow.Tabs;
using TabbedWindow.Windows;

namespace EmberBanner.Editor.GameManagement.Tabs.General
{
    public class GameDataTab : AbstractTab<GameData>
    {
        public GameDataTab(AbstractWindow window, string name, string assetLocation) : base(window, name, assetLocation)
        {
        }
    }
}