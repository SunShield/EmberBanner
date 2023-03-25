using System.Collections.Generic;
using System.Linq;
using TabbedWindow.Tabs;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace TabbedWindow.Windows
{
    public abstract class AbstractWindow : EditorWindow
    {
        protected abstract string Title { get; }
        protected virtual string UxmlPath { get; } = "Assets/Plugins/TabbedWindow/Editor/UXML/Windows/DefaultWindow.uxml";
        protected VisualElement TabButtonsContainer { get; private set; }
        protected VisualElement TabContainer { get; private set; }

        public Dictionary<string, IAbstractTab> Tabs { get; private set; } = new();
        protected Dictionary<string, ToolbarToggle> TabButtons { get; private set; } = new();
        protected IAbstractTab CurrentTab;

        private void OnEnable()
        {
            LoadUml();
            BuildTabs();
            PostOnEnable();
        }

        private void LoadUml()
        {
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(UxmlPath);
            var labelFromUxml = visualTree.CloneTree();
            labelFromUxml.style.flexGrow = 1;
            rootVisualElement.Add(labelFromUxml);
            GetUxmlFields();
        }

        protected virtual void GetUxmlFields()
        {
            TabButtonsContainer = rootVisualElement.Q<VisualElement>("TabButtonsContainer");
            TabContainer = rootVisualElement.Q<VisualElement>("TabContainer");
        }

        private void BuildTabs()
        {
            var tabsList = GetTabs();
            Tabs = tabsList.ToDictionary(tab => tab.Name);
            BuildTabButtons();
            AddTabs();
        }
        
        protected virtual void PostOnEnable() { }

        private void AddTabs()
        {
            var firstActivated = false;
            foreach (var tab in Tabs.Values)
            {
                var tabTyped = tab as VisualElement;
                tabTyped.style.display = DisplayStyle.None;
                if (!firstActivated)
                {
                    SelectTab(tab.Name);
                    firstActivated = true;
                }
                TabContainer.Add(tabTyped);
            }
        }

        private void BuildTabButtons()
        {
            foreach (var tab in Tabs.Values)
            {
                BuildTabButton(tab);
            }
        }

        private void BuildTabButton(IAbstractTab tab)
        {
            var tabButton = new ToolbarToggle();
            tabButton.RegisterValueChangedCallback(ce =>
            {
                if (ce.newValue) SelectTab(tabButton.text);
            });
            tabButton.text = tab.Name;
            tabButton.style.flexGrow = 1f;
            tabButton.style.flexShrink = 1f;
            TabButtons.Add(tab.Name, tabButton);
            TabButtonsContainer.Add(tabButton);
        }

        public void SelectTab(string tabName)
        {
            if (CurrentTab is VisualElement currentTabElement)
            {
                currentTabElement.style.display = DisplayStyle.None;
                TabButtons[CurrentTab.Name].SetValueWithoutNotify(false);
            }

            var newTab = Tabs[tabName] as VisualElement;
            newTab.style.display = DisplayStyle.Flex;
            CurrentTab = Tabs[tabName];
            TabButtons[CurrentTab.Name].SetValueWithoutNotify(true);
        }

        protected abstract List<IAbstractTab> GetTabs();
    }
}