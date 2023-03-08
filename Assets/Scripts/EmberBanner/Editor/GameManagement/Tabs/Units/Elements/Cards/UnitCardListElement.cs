using System.Linq;
using EmberBanner.Core.Models.Units.Cards;
using EmberBanner.Unity.Data.ScriptableObjects.Databases;
using UILibrary.ManagedList.Editor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace EmberBanner.Editor.GameManagement.Tabs.Units.Elements.Cards
{
    public class UnitCardListElement : ManagedListElement<UnitDefaultCardModel, UnitCardListElementData>
    {
        protected override string UxmlKey { get; } = "UnitCardListElement";
        private bool _isHidden = false;
        
        private DropdownField _possibleCardsDropdown;
        private Button        _showHideButton;
        private VisualElement _paramsContainer;
        private VisualElement _spriteElement;
        private IntegerField  _cardsAmountField;
        
        protected override void PostGatherElements()
        {
            _possibleCardsDropdown = Root.Q<DropdownField>("PossibleCardsDropdown");
            _possibleCardsDropdown.choices = GeneralDatabase.EI.Cards.Elements.Keys.ToList();
            if (_possibleCardsDropdown.choices.Count > 0)
            {
                if (string.IsNullOrEmpty(Element.CardName))
                {
                    _possibleCardsDropdown.index = 0;
                    Element.CardName = _possibleCardsDropdown.value;
                }
                else
                {
                    _possibleCardsDropdown.index = _possibleCardsDropdown.choices.IndexOf(Element.CardName);
                }
            }
            
            _showHideButton   = Root.Q<Button>("ShowHideButton");
            _paramsContainer  = Root.Q<VisualElement>("ParamsContainer");
            _spriteElement    = Root.Q<VisualElement>("SpriteElement");
            _spriteElement.style.backgroundImage = new StyleBackground(GeneralDatabase.EI.Cards[Element.CardName].Sprite);
            _cardsAmountField = Root.Q<IntegerField>("CardsAmountField");
            _cardsAmountField.value = Element.Amount;

            ToggleHiddenState();
        }
        
        protected override void PostAddEvents()
        {
            _possibleCardsDropdown.RegisterValueChangedCallback(evt =>
            {
                Element.CardName = _possibleCardsDropdown.value;
                _spriteElement.style.backgroundImage = new StyleBackground(GeneralDatabase.EI.Cards[Element.CardName].Sprite);
            });

            _cardsAmountField.RegisterValueChangedCallback(evt =>
            {
                Element.Amount = evt.newValue;
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