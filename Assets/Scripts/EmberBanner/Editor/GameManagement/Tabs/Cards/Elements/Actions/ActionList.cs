﻿using System;
using System.Linq;
using EmberBanner.Core.Enums.Actions;
using EmberBanner.Core.Models.Actions;
using EmberBanner.Editor.GameManagement.Tabs.Cards.Elements.Actions.TypedElements;
using UILibrary.ManagedList.Editor;
using UnityEngine.UIElements;

namespace EmberBanner.Editor.GameManagement.Tabs.Cards.Elements.Actions
{
    public class ActionList : ManagedList<ActionModel, ActionListData, ActionListElement, ActionListElementData>
    {
        protected override string UxmlKey { get; } = "ActionList";

        private int _currentHighestKey;
        private DropdownField _paramTypeDropdown;

        public ActionType CurrentActionType => Enum.Parse<ActionType>(_paramTypeDropdown.value);

        private string _currentSelectedElementKey;

        protected override void PostGatherElements()
        {
            _paramTypeDropdown = Root.Q<DropdownField>("ActionType");
            
            var values = Enum.GetValues(typeof(ActionType)).Cast<ActionType>();
            var stringValues = values.Select(e => e.ToString()).ToList();
            _paramTypeDropdown.choices = stringValues;
            _paramTypeDropdown.index = 0;
        }

        protected override void PostAddElement(string elementKey)
        {
            _currentHighestKey++;
        }

        protected override ActionListElement CreateListElementInstance(ActionModel element)
        {
            ActionListElement listElement = element.Type switch
            {
                ActionType.Aggression => new AggressionActionListElement(),
                ActionType.Defense => new DefenseActionListElement(),
                ActionType.Support => new SupportActionListElement(),
                _ => null
            };

            listElement.onSelectionAreaClicked += SelectElement;

            return listElement;
        }

        protected override string GetElementKey() => (_currentHighestKey + 1).ToString();
        protected override string GetStringKey(ActionModel value) => value.Name;
        protected override ActionListElementData CreateElementData(string elementKey) => new();

        protected override void PostUpdate()
        {
            var elements = GetValuesPool();
            _currentHighestKey = elements.Count != 0 
                ? elements.Select(e => int.Parse(e.Name)).Max() 
                : 0;
        }

        private void SelectElement(string elementKey)
        {
            if (_currentSelectedElementKey != null && Elements.ContainsKey(_currentSelectedElementKey))
                Elements[_currentSelectedElementKey].SetSelected(false);
            
            if (_currentSelectedElementKey == elementKey)
            {
                _currentSelectedElementKey = null;
            }
            else
            {
                _currentSelectedElementKey = elementKey;
                Elements[_currentSelectedElementKey].SetSelected(true);
                
            }
            onElementSelectionChanged?.Invoke(_currentSelectedElementKey);
        }

        public event Action<string> onElementSelectionChanged;
    }
}