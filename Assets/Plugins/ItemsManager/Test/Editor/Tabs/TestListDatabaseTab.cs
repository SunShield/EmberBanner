using ItemsManager.Editor.Tabs;
using ItemsManager.Test.Databases;
using ItemsManager.Test.Editor.Tabs.Elements;
using TabbedWindow.Windows;

namespace ItemsManager.Test.Editor.Tabs
{
    public class TestListDatabaseTab 
        : ListDatabaseManagerTab<TestListAbstractDatabaseElement,TestNavigatorElement, 
            TestListDatabase, TestGeneralDatabase, TestElementInspector, TestListNavigator>
    {
        public TestListDatabaseTab(AbstractWindow window, string name, string assetLocation) : base(window, name, assetLocation)
        {
        }

        protected override TestListDatabase GetDatabase() => GeneralDatabase.ListDb;
    }
}