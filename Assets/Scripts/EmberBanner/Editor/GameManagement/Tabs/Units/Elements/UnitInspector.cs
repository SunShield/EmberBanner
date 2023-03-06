using EmberBanner.Core.Models.Units;
using EmberBanner.Unity.Data.ScriptableObjects.Databases;
using NFate.Editor.EditorElements;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace EmberBanner.Editor.GameManagement.Tabs.Units.Elements
{
    public class UnitInspector : InspectorWithUxml<UnitModel, UnitsDatabase>
    {
        protected override string UxmlKey { get; } = "UnitInspector";
        
        private Label _elementName;
        private VisualElement _icon;
        private ObjectField _iconPicker;
        private IntegerField _startingHealthField;
        private IntegerField _maxHealthField;
        private IntegerField _startingEnergyField;
        private IntegerField _maxEnergyField;

        public UnitInspector() : base()
        {
            BuildGeometry();
            AddEvents();
        }

        private void BuildGeometry()
        {
            _elementName          = Root.Q<Label>("ElementName");
            _icon                 = Root.Q<VisualElement>("Icon");
            _iconPicker           = Root.Q<ObjectField>("IconPicker");
            _startingHealthField  = Root.Q<IntegerField>("StartingHealthField");
            _maxHealthField       = Root.Q<IntegerField>("MaxHealthField");
            _startingEnergyField  = Root.Q<IntegerField>("StartingEnergyField");
            _maxEnergyField       = Root.Q<IntegerField>("MaxEnergyField");
        }

        private void AddEvents()
        {
            _iconPicker.RegisterValueChangedCallback(evt =>
            {
                InspectedElement.Sprite = evt.newValue as Sprite;
                _icon.style.backgroundImage = new StyleBackground(InspectedElement.Sprite);
                PostElementUpdate();
            });

            _startingHealthField.RegisterValueChangedCallback(evt =>
            {
                InspectedElement.StartingHealth = evt.newValue;
            });

            _maxHealthField.RegisterValueChangedCallback(evt =>
            {
                InspectedElement.MaxHealth = evt.newValue;
            });

            _startingEnergyField.RegisterValueChangedCallback(evt =>
            {
                InspectedElement.StartingEnergy = evt.newValue;
            });

            _maxEnergyField.RegisterValueChangedCallback(evt =>
            {
                InspectedElement.MaxEnergy = evt.newValue;
            });
        }
        
        protected override void PostPrepare()
        {
            
        }

        protected override void OnElementSet()
        {
            _elementName.text = InspectedElement.Name;
            _icon.style.backgroundImage = new StyleBackground(InspectedElement.Sprite);
            _iconPicker.value = InspectedElement.Sprite;
            _startingHealthField.value = InspectedElement.StartingHealth;
            _maxHealthField.value = InspectedElement.MaxHealth;
            _startingEnergyField.value = InspectedElement.StartingEnergy;
            _maxEnergyField.value = InspectedElement.MaxEnergy;
        }
    }
}