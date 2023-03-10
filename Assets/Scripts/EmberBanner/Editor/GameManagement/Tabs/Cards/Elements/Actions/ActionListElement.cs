using System;
using System.Linq;
using EmberBanner.Core.Enums.Actions;
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
        private   IntegerField    _magnitudeField;
        protected DropdownField   MagnitudeTypeDropdown { get; private set; }
        private   IntegerField    _coinsField;
        protected IntegerField    ThresholdField { get; private set; }
        private   IntegerField    _spanField;
        private   IntegerField    _growthField;
        private   IntegerField    _cpMinField;
        private   IntegerField    _cpMaxField;
        private   TextField       _descriptionField;
        private   VisualElement   _listContainer;
        private   ActionParamList _paramsList;
        private   VisualElement   _topRowContainer;
        protected DropdownField   PossibleTargetsField;
        
        protected override void PostGatherElements()
        {
            _paramsContainer      = Root.Q<VisualElement>("ParamsContainer");
            
            _typeColorElement     = Root.Q<VisualElement>("TypeColorElement");
            _showHideButton       = Root.Q<Button>("ShowHideButton");
            _magnitudeField       = Root.Q<IntegerField>("MagnitudeField");
            _magnitudeField.style.fontSize = 32f;
            MagnitudeTypeDropdown = Root.Q<DropdownField>("MagnitudeTypeDropdown");

            _coinsField    = Root.Q<IntegerField>("CoinsField");
            _spanField     = Root.Q<IntegerField>("GrowSpanField");
            _growthField   = Root.Q<IntegerField>("GrowGrowthField");
            ThresholdField = Root.Q<IntegerField>("ThresholdField");
            _cpMinField    = Root.Q<IntegerField>("CpMinField");
            _cpMaxField    = Root.Q<IntegerField>("CpMaxField");

            _descriptionField = Root.Q<TextField>("DescriptionField");
            _topRowContainer = Root.Q<VisualElement>("TopRowContainer");
            PossibleTargetsField = new DropdownField();
            PossibleTargetsField.label = "Targets";
            _topRowContainer.Insert(1, PossibleTargetsField);

            _listContainer = Root.Q<VisualElement>("ListContainer");
            _paramsList = CreateActionParamsList();
            _listContainer.Add(_paramsList);
        }

        private ActionParamList CreateActionParamsList()
        {
            var data = new ActionParamListData()
            {
                ValuesPoolGetter = () => Element.Params.Values.ToList(),
                ElementByKeyGetter = key => Element.Params[key],
                ElementInListPredicate = el => true,
                OnAddElementClickedCallback = AddElement,
                OnRemoveElementClickedCallback = RemoveElement
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

        protected override void PostInitialize()
        {
            _typeColorElement.style.backgroundColor = GetTypeColor();
            
            _magnitudeField.value   = Element.Magnitude;
            _coinsField.value       = Element.CoinsAmount;
            _spanField.value        = Element.ClashingPowerGrowthRate.Span;
            _growthField.value      = Element.ClashingPowerGrowthRate.Growth;
            ThresholdField.value    = Element.Threshold;
            _cpMinField.value       = Element.ClashingPower.Min;
            _cpMaxField.value       = Element.ClashingPower.Max;
            _descriptionField.value = Element.RawDescription;
            
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
            });

            _coinsField.RegisterValueChangedCallback(evt =>
            {
                Element.CoinsAmount = evt.newValue;
            });

            _spanField.RegisterValueChangedCallback(evt =>
            {
                Element.ClashingPowerGrowthRate.Span = evt.newValue;
            });

            _growthField.RegisterValueChangedCallback(evt =>
            {
                Element.ClashingPowerGrowthRate.Growth = evt.newValue;
            });

            ThresholdField.RegisterValueChangedCallback(evt =>
            {
                Element.Threshold = evt.newValue;
            });

            _cpMinField.RegisterValueChangedCallback(evt =>
            {
                Element.ClashingPower.Min = evt.newValue;
            });

            _cpMaxField.RegisterValueChangedCallback(evt =>
            {
                Element.ClashingPower.Max = evt.newValue;
            });

            _descriptionField.RegisterValueChangedCallback(evt =>
            {
                Element.RawDescription = evt.newValue;
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

        protected void FireOnPossibleTargetsChangedEvent() => onPossibleTargetsChanged?.Invoke();

        public event Action onPossibleTargetsChanged;
    }
}