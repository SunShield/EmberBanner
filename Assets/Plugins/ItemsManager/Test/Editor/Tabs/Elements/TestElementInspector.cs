using ItemsManager.Editor.Tabs.Elements.Inspectors;
using ItemsManager.Test.Databases;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace ItemsManager.Test.Editor.Tabs.Elements
{
    public class TestElementInspector : AbstractInspector<TestListAbstractDatabaseElement, TestListDatabase>
    {
        private const string EditorUxmlPath = @"Assets/Plugins/ItemsManager/Uxml/TestElementInspector.uxml";
        
        private VisualElement _mainContainer;
        private Label _elementName;
        private IntegerField _aField;
        private IntegerField _bField;
        private VisualElement _icon;
        private ObjectField _iconPicker;

        protected override void PostPrepare()
        {
            BuildGeometry();

            _aField.RegisterValueChangedCallback(evt =>
            {
                InspectedElement.A = evt.newValue; 
                PostElementUpdate();
            });
            _bField.RegisterValueChangedCallback(evt =>
            {
                InspectedElement.B = evt.newValue; 
                PostElementUpdate();
            });
            _iconPicker.RegisterValueChangedCallback(evt =>
            {
                InspectedElement.Icon = evt.newValue as Sprite;
                _icon.style.backgroundImage = new StyleBackground(InspectedElement.Icon);
                PostElementUpdate();
            });
        }

        private void BuildGeometry()
        {
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(EditorUxmlPath);
            _mainContainer = visualTree.CloneTree();
            _mainContainer.style.flexGrow = 1f;
            Add(_mainContainer);

            _elementName = _mainContainer.Q<Label>("ElementName");
            _aField = _mainContainer.Q<IntegerField>("AField");
            _bField = _mainContainer.Q<IntegerField>("BField");
            _icon = _mainContainer.Q<VisualElement>("Icon");
            _iconPicker = _mainContainer.Q<ObjectField>("IconPicker");
        }

        protected override void OnElementSet()
        {
            _elementName.text = InspectedElement.Name;
            _aField.value = InspectedElement.A;
            _bField.value = InspectedElement.B;
            _icon.style.backgroundImage = new StyleBackground(InspectedElement.Icon);
            _iconPicker.value = InspectedElement.Icon;
        }
    }
}