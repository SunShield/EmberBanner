using System.Linq;
using EmberBanner.Core.Models.Battles;
using EmberBanner.Unity.Data.ScriptableObjects.Databases;
using UILibrary.ManagedList.Editor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace EmberBanner.Editor.GameManagement.Tabs.Battles.Elements.BattleUnits
{
    public class BattleUnitListElement : ManagedListElement<UnitInBattleModel, BattleUnitListElementData>
    {
        protected override string UxmlKey { get; } = "BattleUnitListElement";
        private bool _isHidden = false;

        private DropdownField _possibleUnitsDropdown;
        private Button        _showHideButton;
        private VisualElement _paramsContainer;
        private IntegerField  _waveField;

        protected override void PostGatherElements()
        {
            _possibleUnitsDropdown = Root.Q<DropdownField>("PossibleUnitsDropdown");
            _possibleUnitsDropdown.choices = GeneralDatabase.EI.Units.Elements.Keys.ToList();
            if (_possibleUnitsDropdown.choices.Count > 0)
            {
                if (string.IsNullOrEmpty(Element.UnitName))
                {
                    _possibleUnitsDropdown.index = 0;
                    Element.UnitName = _possibleUnitsDropdown.value;
                }
                else
                {
                    _possibleUnitsDropdown.index = _possibleUnitsDropdown.choices.IndexOf(Element.UnitName);
                }
            }
            
            _showHideButton  = Root.Q<Button>("ShowHideButton");
            _paramsContainer = Root.Q<VisualElement>("ParamsContainer");
            _waveField       = Root.Q<IntegerField>("WaveField");
            _waveField.value = Element.Wave;

            ToggleHiddenState();
        }

        protected override void PostAddEvents()
        {
            _possibleUnitsDropdown.RegisterValueChangedCallback(evt =>
            {
                Element.UnitName = _possibleUnitsDropdown.value;
                Update();
            });

            _showHideButton.clicked += ToggleHiddenState;

            _waveField.RegisterValueChangedCallback(evt =>
            {
                Element.Wave = evt.newValue;
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
    }
}