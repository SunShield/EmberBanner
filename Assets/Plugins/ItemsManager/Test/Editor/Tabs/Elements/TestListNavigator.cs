using System;
using ItemsManager.Editor.Tabs.Elements.Navigators;
using ItemsManager.Test.Databases;
using UnityEditor;
using UnityEngine.UIElements;

namespace ItemsManager.Test.Editor.Tabs.Elements
{
    public class TestListNavigator : ListNavigator<TestListAbstractDatabaseElement, TestNavigatorElement, TestListDatabase>
    {
        private const string NavigatorUxmlPath = @"Assets/Plugins/ItemsManager/Uxml/ListNavigator.uxml";

        private VisualElement _mainContainer;
        private TextField _newElementNameField;
        private Button _addButton;
        private Button _removeButton;
        private ScrollView _scrollView;
        
        public TestListNavigator()
        {
            BuildGeometry();
            AddEvents();
        }

        private void BuildGeometry()
        {
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(NavigatorUxmlPath);
            _mainContainer = visualTree.CloneTree();
            _mainContainer.style.width = 350f;
            Add(_mainContainer);

            _newElementNameField = _mainContainer.Q<TextField>("NewElementNameField");
            _addButton = _mainContainer.Q<Button>("AddButton");
            _removeButton = _mainContainer.Q<Button>("RemoveButton");
            _scrollView = _mainContainer.Q<ScrollView>("ElementsContainer");
        }

        private void AddEvents()
        {
            _addButton.clicked += () =>
            {
                var name = _newElementNameField.text;
                if(string.IsNullOrEmpty(name)) return;
                
                TryAddElement(name);
            };
            _removeButton.clicked += () =>
            {
                if (CurrentElement == null) return;

                _scrollView.Remove(CurrentNavigatorElement);
                RemoveElement(CurrentNavigatorElement);
            };
        }

        protected override TestListAbstractDatabaseElement ConstructElement(string name)
            => Activator.CreateInstance(typeof(TestListAbstractDatabaseElement), name) as TestListAbstractDatabaseElement;

        protected override void PostCreateNavigatorElement(TestListAbstractDatabaseElement element, TestNavigatorElement navigatorElement)
        {
            _scrollView.Add(navigatorElement);
        }
    }
}