using System;
using System.Linq;
using EmberBanner.Core.Enums.Actions;
using EmberBanner.Core.Enums.Battle.Targeting;
using EmberBanner.Core.Models.Actions;
using EmberBanner.Core.Models.Actions.Params;
using EmberBanner.Editor.GameManagement.Tabs.Cards.Elements.Actions.Params;
using UILibrary.ManagedList.Editor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace EmberBanner.Editor.GameManagement.Tabs.Cards.Elements.Actions
{
    public class ActionListElement : ManagedListElement<ActionModel, ActionListElementData>
    {
        protected override string UxmlKey { get; } = "ActionListElement";
        private bool _isHidden = false;

        private   Button          _showHideButton;
        private   VisualElement   _typeColorElement;
        private   VisualElement   _paramsContainer;
        protected VisualElement   MagnitudeTypeSpriteElement;
        private   IntegerField    _magnitudeField;
        protected DropdownField   MagnitudeTypeDropdown { get; private set; }
        private   IntegerField    _coinsField;
        protected IntegerField    ThresholdField { get; private set; }
        private   IntegerField    _clashLoseHandicapField;
        private   IntegerField    _cpMinField;
        private   IntegerField    _cpMaxField;
        private   TextField       _descriptionField;
        private   VisualElement   _listContainer;
        private   ActionParamList _paramsList;
        private   VisualElement   _topRowContainer;
        protected DropdownField   PossibleTargetsField;
        private   Label           _selectedLabel;

        public bool IsSelected { get; private set; }
        
        protected override void PostGatherElements()
        {
            _paramsContainer           = Root.Q<VisualElement>("ParamsContainer");
            _typeColorElement          = Root.Q<VisualElement>("TypeColorElement");
            _showHideButton            = Root.Q<Button>("ShowHideButton");
            MagnitudeTypeSpriteElement = Root.Q<VisualElement>("MagnitudeTypeSpriteElement");
            _magnitudeField            = Root.Q<IntegerField>("MagnitudeField");
            MagnitudeTypeDropdown      = Root.Q<DropdownField>("MagnitudeTypeDropdown");
            _coinsField                = Root.Q<IntegerField>("CoinsField");
            _clashLoseHandicapField    = Root.Q<IntegerField>("ClashLoseHandicapField");
            ThresholdField             = Root.Q<IntegerField>("ThresholdField");
            _cpMinField                = Root.Q<IntegerField>("CpMinField");
            _cpMaxField                = Root.Q<IntegerField>("CpMaxField");
            _descriptionField          = Root.Q<TextField>("DescriptionField");
            _topRowContainer           = Root.Q<VisualElement>("TopRowContainer");
            _listContainer             = Root.Q<VisualElement>("ListContainer");
            PossibleTargetsField       = Root.Q<DropdownField>("PossibleTargetsField");
            _selectedLabel             = Root.Q<Label>("SelectedLabel");
            
            _magnitudeField.style.fontSize = 32f;
            _topRowContainer.Insert(1, PossibleTargetsField);

            _paramsList = CreateActionParamsList();
            _listContainer.Add(_paramsList);

            AddManipulators();
        }

        private ActionParamList CreateActionParamsList()
        {
            var data = new ActionParamListData()
            {
                ValuesPoolGetter = () => Element.Params.Values.ToList(),
                ElementByKeyGetter = key => Element.Params[key],
                ElementInListPredicate = el => true,
                OnAddElementClickedCallback = AddElement,
                OnRemoveElementClickedCallback = RemoveElement,
                ElementUpdateCallback = Update
            };

            void AddElement(string elementKey)
            {
                var element = new ActionParamModel()
                {
                    Name = elementKey,
                    Type = Enum.Parse<ActionParamType>(_paramsList.CurrentParamType)
                };
                
                Element.Params.Add(element.Name, element);
            }

            void RemoveElement(string elementKey)
            {
                Element.Params.Remove(elementKey);
            }

            var list = new ActionParamList();
            list.Initialize(data);
            return list;
        }

        private void AddManipulators()
        {
            _typeColorElement.AddManipulator(new Clickable(OnSelectionAreaClicked));
        }

        protected override void PostInitialize()
        {
            _typeColorElement.style.backgroundColor = GetTypeColor();
            
            _magnitudeField.value         = Element.Magnitude;
            _coinsField.value             = Element.CoinsAmount;
            _clashLoseHandicapField.value = Element.ClashLoseHandicap;
            ThresholdField.value          = Element.Threshold;
            _cpMinField.value             = Element.ClashingPower.Min;
            _cpMaxField.value             = Element.ClashingPower.Max;
            _descriptionField.value       = Element.RawDescription;
            
            PossibleTargetsField.choices = Enum.GetValues(typeof(TargetType)).Cast<TargetType>().Select(e => e.ToString()).ToList();
            PossibleTargetsField.index = (int)Element.PossibleTargets;
            
            ToggleHiddenState();
            _paramsList.Update();
        }

        protected Color GetTypeColor() => Element.Type switch
        {
            ActionType.Aggression => new Color(0.9f, 0.2f, 0.2f, 1f),
            ActionType.Defense    => new Color(0.1f, 0.5f, 0.9f, 1f),
            ActionType.Support    => new Color(0.4f, 0.8f, 0.4f, 1f),
            _ => throw new ArgumentOutOfRangeException()
        };

        protected override void PostAddEvents()
        {
            _showHideButton.clicked += ToggleHiddenState;

            _magnitudeField.RegisterValueChangedCallback(evt =>
            {
                Element.Magnitude = evt.newValue;
                Update();
            });

            _coinsField.RegisterValueChangedCallback(evt =>
            {
                Element.CoinsAmount = evt.newValue;
                Update();
            });

            _clashLoseHandicapField.RegisterValueChangedCallback(evt =>
            {
                Element.ClashLoseHandicap = evt.newValue;
                Update();
            });

            ThresholdField.RegisterValueChangedCallback(evt =>
            {
                Element.Threshold = evt.newValue;
                Update();
            });

            _cpMinField.RegisterValueChangedCallback(evt =>
            {
                Element.ClashingPower.Min = evt.newValue;
                Update();
            });

            _cpMaxField.RegisterValueChangedCallback(evt =>
            {
                Element.ClashingPower.Max = evt.newValue;
                Update();
            });

            _descriptionField.RegisterValueChangedCallback(evt =>
            {
                Element.RawDescription = evt.newValue;
                Update();
            });
            
            PossibleTargetsField.RegisterValueChangedCallback(evt =>
            {
                Element.PossibleTargets = PossibleTargetsField.index;
                Update();
            });
        }

        private void ToggleHiddenState()
        {
            _isHidden = !_isHidden;
            if (!_isHidden) Root.Add(_paramsContainer);
            else Root.Remove(_paramsContainer);

            _showHideButton.text = _isHidden
                ? "Show"
                : "Hide";
        }

        public void SetSelected(bool selected)
        {
            IsSelected = selected;
            _selectedLabel.text = selected ? "X" : "";
        }

        private void OnSelectionAreaClicked()
        {
            onSelectionAreaClicked?.Invoke(Element.Name);
        }

        public event Action<string> onSelectionAreaClicked;
    }
}