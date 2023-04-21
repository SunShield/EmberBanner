using System;
using UILibrary.UxmlElement.Editor;
using UnityEngine;
using UnityEngine.UIElements;

namespace EmberBanner.Editor.GameManagement.Tabs.Cards.Elements.Actions.Events
{
    public abstract class ActionEventInspector : VisualElementWithUxml
    {
        private readonly Color ActiveColor = new (0.2f, 1f, 0.2f, 1f);
        private readonly Color InactiveColor = new (1f, 0.2f, 0.2f, 1f);
        private readonly Color SelectedColor = new (0.3f, 0.5f, 0.9f, 1f);
        
        protected VisualElement ActiveInactiveToggle { get; private set; }
        protected Label         NameLabel            { get; private set; }

        private StyleColor _unselectedColor;
        private int _eventIndex;
        private bool _isActive;
        
        protected ActionEventInspector(string eventName, int eventIndex, bool active)
        {
            _eventIndex = eventIndex;
            _isActive = active;
            BuildGeometry(eventName);
            AddManipulators();
        }

        private void BuildGeometry(string eventName)
        {
            ActiveInactiveToggle = Root.Q<VisualElement>("ActiveButton");
            SetActiveColor();
            
            NameLabel = Root.Q<Label>("NameLabel");
            _unselectedColor = NameLabel.style.backgroundColor;
            NameLabel.text = eventName;
        }

        protected void SetActiveColor() => ActiveInactiveToggle.style.backgroundColor = _isActive ? ActiveColor : InactiveColor;

        private void AddManipulators()
        {
            ActiveInactiveToggle.AddManipulator(new Clickable(OnActiveInactiveToggleClick));
            NameLabel.AddManipulator(new Clickable(OnLabelClicked));
        }

        private void OnActiveInactiveToggleClick()
        {
            _isActive = !_isActive;
            SetActiveColor();
            onActiveStateChanged?.Invoke(_eventIndex);
        }

        private void OnLabelClicked()
        {
            onClick?.Invoke(this);
        }

        public void SetSelected(bool selected)
        {
            NameLabel.style.backgroundColor = selected ? SelectedColor : _unselectedColor;
        }

        public event Action<int> onActiveStateChanged;
        public event Action<ActionEventInspector> onClick;
    }
}