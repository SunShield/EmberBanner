using System.Linq;
using EmberBanner.Core.Models.Units;
using EmberBanner.Core.Models.Units.Cards;
using EmberBanner.Core.Models.Units.Crystals;
using EmberBanner.Editor.GameManagement.Tabs.Units.Elements.Cards;
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
        private IntegerField  _healthRegenField;
        private IntegerField  _startingWillField;
        private IntegerField  _maxWillField;
        private IntegerField  _willRegenField;
        private IntegerField  _startingEnergyField;
        private IntegerField  _maxEnergyField;
        private IntegerField  _energyRegenField;
        private IntegerField  _handSizeField;
        private IntegerField  _drawField;
        private Toggle        _isEnemyToggle;
        private VisualElement _crystalsContainer;
        private CrystalList   _crystalList;
        private VisualElement _cardsContainer;
        private UnitCardList  _unitUnitCardList;

        public UnitInspector() : base()
        {
            BuildGeometry();
            AddEvents();
        }

        private void BuildGeometry()
        {
            style.flexGrow = 1f;
            _elementName          = Root.Q<Label>("ElementName");
            _icon                 = Root.Q<VisualElement>("Icon");
            _iconPicker           = Root.Q<ObjectField>("IconPicker");
            
            _startingHealthField  = Root.Q<IntegerField>("StartingHealthField");
            _maxHealthField       = Root.Q<IntegerField>("MaxHealthField");
            _healthRegenField     = Root.Q<IntegerField>("HealthRegenField");
            
            _startingWillField    = Root.Q<IntegerField>("StartingWillField");
            _maxWillField         = Root.Q<IntegerField>("MaxWillField");
            _willRegenField       = Root.Q<IntegerField>("WillRegenField");
            
            _startingEnergyField  = Root.Q<IntegerField>("StartingEnergyField");
            _maxEnergyField       = Root.Q<IntegerField>("MaxEnergyField");
            _energyRegenField     = Root.Q<IntegerField>("EnergyRegenField");
            
            _handSizeField        = Root.Q<IntegerField>("HandSizeField");
            _drawField            = Root.Q<IntegerField>("DrawField");
            
            _isEnemyToggle        = Root.Q<Toggle>("IsEnemyToggle");
            _crystalsContainer    = Root.Q<VisualElement>("CrystalsContainer");
            _cardsContainer       = Root.Q<VisualElement>("CardsContainer");
            _cardsContainer.style.display = DisplayStyle.None;
            
            AddCrystalsList();
            AddUnitCardsList();
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

            _healthRegenField.RegisterValueChangedCallback(evt =>
            {
                InspectedElement.HealthRegen = evt.newValue;
            });
            
            _startingWillField.RegisterValueChangedCallback(evt =>
            {
                InspectedElement.StartingWill = evt.newValue;
            });

            _maxWillField.RegisterValueChangedCallback(evt =>
            {
                InspectedElement.MaxWill = evt.newValue;
            });

            _willRegenField.RegisterValueChangedCallback(evt =>
            {
                InspectedElement.WillRegen = evt.newValue;
            });

            _startingEnergyField.RegisterValueChangedCallback(evt =>
            {
                InspectedElement.StartingEnergy = evt.newValue;
            });

            _maxEnergyField.RegisterValueChangedCallback(evt =>
            {
                InspectedElement.MaxEnergy = evt.newValue;
            });

            _energyRegenField.RegisterValueChangedCallback(evt =>
            {
                InspectedElement.EnergyRegen = evt.newValue;
            });

            _handSizeField.RegisterValueChangedCallback(evt =>
            {
                InspectedElement.HandSize = evt.newValue;
            });

            _drawField.RegisterValueChangedCallback(evt =>
            {
                InspectedElement.Draw = evt.newValue;
            });

            _isEnemyToggle.RegisterValueChangedCallback(evt =>
            {
                InspectedElement.IsEnemyUnit = evt.newValue;
                _cardsContainer.style.display = InspectedElement.IsEnemyUnit ? DisplayStyle.Flex : DisplayStyle.None;
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

        private void AddUnitCardsList()
        {
            var data = new UnitCardListData()
            {
                ValuesPoolGetter = () => InspectedElement.DefaultCards,
                ElementByKeyGetter = key => InspectedElement.DefaultCards.First(crystal => crystal.Name == key),
                ElementInListPredicate = card => true,
                OnAddElementClickedCallback = AddCard,
                OnRemoveElementClickedCallback = RemoveCard
            };

            void AddCard(string cardName)
            {
                InspectedElement.DefaultCards.Add(new UnitDefaultCardModel() { Name = cardName, Amount = 1 });
                Database.Update();
            }

            void RemoveCard(string cardName)
            {
                for (int i = 0; i < InspectedElement.DefaultCards.Count; i++)
                {
                    if (InspectedElement.DefaultCards[i].Name == cardName)
                    {
                        InspectedElement.DefaultCards.RemoveAt(i);
                        return;
                    }
                }
                Database.Update();
            }
            
            _unitUnitCardList = new UnitCardList();
            _unitUnitCardList.Initialize(data);
            _cardsContainer.Add(_unitUnitCardList);
        }
        
        protected override void PostPrepare()
        {
            
        }

        protected override void OnElementSet()
        {
            Update();
        }

        public void Update()
        {
            _elementName.text = InspectedElement.Name;
            _icon.style.backgroundImage = new StyleBackground(InspectedElement.Sprite);
            _iconPicker.value = InspectedElement.Sprite;
            
            _startingHealthField.value = InspectedElement.StartingHealth;
            _maxHealthField.value = InspectedElement.MaxHealth;
            _healthRegenField.value = InspectedElement.HealthRegen;
            
            _startingWillField.value = InspectedElement.StartingWill;
            _maxWillField.value = InspectedElement.MaxWill;
            _willRegenField.value = InspectedElement.WillRegen;
            
            _startingEnergyField.value = InspectedElement.StartingEnergy;
            _maxEnergyField.value = InspectedElement.MaxEnergy;
            _energyRegenField.value = InspectedElement.EnergyRegen;
            
            _handSizeField.value = InspectedElement.HandSize;
            _drawField.value = InspectedElement.Draw;
            _isEnemyToggle.value = InspectedElement.IsEnemyUnit;
            _cardsContainer.style.display = InspectedElement.IsEnemyUnit ? DisplayStyle.Flex : DisplayStyle.None;

            _crystalList.Update();
            _unitUnitCardList.Update();
        }
    }
}