using System.Linq;
using EmberBanner.Core.Models.Units.Cards;
using EmberBanner.Unity.Data.ScriptableObjects.Databases;
using UILibrary.ManagedList.Editor;
using UnityEngine.UIElements;

namespace EmberBanner.Editor.GameManagement.Tabs.Units.Elements.Cards
{
    public class UnitCardListElement : ManagedListElement<UnitDefaultCardModel, UnitCardListElementData>
    {
        protected override string UxmlKey { get; } = "CardListElement";
        private bool _isHidden = false;
        
        private DropdownField _possibleValuesDropdown;
        private Button        _showHideButton;
        private VisualElement _paramsContainer;
        
        protected override void PostGatherElements()
        {
            _possibleValuesDropdown = Root.Q<DropdownField>("PossibleValuesDropdown");
            _possibleValuesDropdown.choices = GeneralDatabase.EI.Units.Elements.Keys.ToList();
            if (_possibleValuesDropdown.choices.Count > 0)
            {
                _possibleValuesDropdown.index = 0;
                Element.CardName = _possibleValuesDropdown.value;
            }
            
            _showHideButton  = Root.Q<Button>("ShowHideButton");
            _paramsContainer = Root.Q<VisualElement>("ParamsContainer");

            ToggleHiddenState();
        }
        
        protected override void PostAddEvents()
        {
            _possibleValuesDropdown.RegisterValueChangedCallback(evt =>
            {
                Element.CardName = _possibleValuesDropdown.value;
            });

            _showHideButton.clicked += ToggleHiddenState;
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
    }
}