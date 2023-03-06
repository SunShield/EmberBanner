using ItemsManager.Editor.Tabs.Elements.Navigators;
using ItemsManager.Test.Databases;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace ItemsManager.Test.Editor.Tabs.Elements
{
    public class TestNavigatorElement : AbstractNavigatorElement<TestListAbstractDatabaseElement>
    {
        private const string NavigatorElementUxmlPath = @"Assets/Plugins/ItemsManager/Uxml/NavigatorElement.uxml";
        private static readonly Color SelectedColor = new Color(0.4f, 0.7f, 0.9f, 1f);
        
        private Color _defaultBgColor;
        
        protected VisualElement _icon;
        protected Label _elementName;
        
        public TestNavigatorElement()
        {
            style.flexShrink = 1f;
            style.flexGrow = 1f;
            style.height = 50f;
            
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(NavigatorElementUxmlPath);
            var treeClone = visualTree.CloneTree();
            var root = treeClone.Q<VisualElement>("Root");
            Add(root);

            _icon = root.Q<VisualElement>("Icon");
            _icon.RegisterCallback<GeometryChangedEvent>(evt =>
            {
                _icon.style.width = _icon.resolvedStyle.height;
            });
            
            _elementName = root.Q<Label>("ElementName");

            _defaultBgColor = style.backgroundColor.value;
        }

        protected override void DoBindElement()
        {
            _elementName.text = WrappedElement.Name;
            _icon.style.backgroundImage = new StyleBackground(WrappedElement.Icon);
        }

        public override void SetSelected(bool selected)
        {
            style.backgroundColor = !selected ? _defaultBgColor : SelectedColor;
        }

        public override void DoUpdateElement()
        {
            _icon.style.backgroundImage = new StyleBackground(WrappedElement.Icon);
        }
    }
}