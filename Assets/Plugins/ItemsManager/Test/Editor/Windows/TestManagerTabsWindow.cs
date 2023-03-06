using System.Collections.Generic;
using ItemsManager.Test.Editor.Tabs;
using TabbedWindow.Tabs;
using TabbedWindow.Windows;
using UnityEditor;
using UnityEngine;

namespace ItemsManager.Test.Editor.Windows
{
    public class TestManagerTabsWindow : AbstractWindow
    {
        //[MenuItem("Tabbed Windows/Test/Manager Window")]
        public static void Show()
        {
            var window = GetWindow<TestManagerTabsWindow>();
            window.titleContent = new GUIContent("Manager Window");
        }
        
        protected override string Title { get; } = "Test Manager";
        
        protected override List<IAbstractTab> GetTabs()
        {
            var tab1 = new TestListDatabaseTab(this, "Manager 1", @"Assets/Plugins/ItemsManager/Test/Data/TestGeneralDb.asset");
            tab1.Prepare();
            
            var tab2 = new TestListDatabaseTab(this, "Manager 2", @"Assets/Plugins/ItemsManager/Test/Data/TestGeneralDb.asset");
            tab2.Prepare();
            
            return new List<IAbstractTab>()
            {
                tab1, tab2
            };
        }
    }
}