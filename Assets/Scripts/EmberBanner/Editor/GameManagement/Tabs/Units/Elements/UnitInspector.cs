using System.Linq;
using EmberBanner.Core.Models.Units;
using EmberBanner.Core.Models.Units.Crystals;
using EmberBanner.Editor.GameManagement.Tabs.Units.Elements.Crystals;
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
        
        private Label         _elementName;
        private VisualElement _icon;
        private ObjectField   _iconPicker;
        private IntegerField  _startingHealthField;
        private IntegerField  _maxHealthField;
        private IntegerField  _startingEnergyField;
        private IntegerField  _maxEnergyField;
        private VisualElement _crystalsContainer;
        private CrystalList   _crystalList;

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
            _crystalsContainer    = Root.Q<VisualElement>("CrystalsContainer");
            AddCrystalsList();
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

        private void AddCrystalsList()
        {
            var data = new CrystalListData()
            {
                ValuesPoolGetter = () => InspectedElement.Crystals,
                ElementByKeyGetter = key => InspectedElement.Crystals.First(crystal => crystal.Name == key),
                ElementInListPredicate = crystal => true,
                OnAddElementClickedCallback = AddCrystal,
                OnRemoveElementClickedCallback = RemoveCrystal
            };

            void AddCrystal(string crystalName)
            {
                InspectedElement.Crystals.Add(new UnitCrystalModel() { Name = crystalName });
                Database.Update();
            }

            void RemoveCrystal(string crystalName)
            {
                for (int i = 0; i < InspectedElement.Crystals.Count; i++)
                {
                    if (InspectedElement.Crystals[i].Name == crystalName)
                    {
                        InspectedElement.Crystals.RemoveAt(i);
                        return;
                    }
                }
                Database.Update();
            }
            
            _crystalList = new CrystalList();
            _crystalList.Initialize(data);
            _crystalsContainer.Add(_crystalList);
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
            
            _crystalList.Update();
        }
    }
}