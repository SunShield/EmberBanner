using EmberBanner.Core.Models.Units.Crystals;
using UILibrary.ManagedList.Editor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace EmberBanner.Editor.GameManagement.Tabs.Units.Elements.Crystals
{
    public class CrystalListElement : ManagedListElement<UnitCrystalModel, CrystalListElementData>
    {
        protected override string UxmlKey { get; } = "CrystalListElement";

        private IntegerField _minRollBoundField;
        private IntegerField _maxRollBoundField;

        protected override void PostGatherElements()
        {
            _minRollBoundField = Root.Q<IntegerField>("MinRollBoundField");
            _maxRollBoundField = Root.Q<IntegerField>("MaxRollBoundField");
        }

        protected override void PostAddEvents()
        {
            _minRollBoundField.RegisterValueChangedCallback(evt =>
            {
                Element.RollBounds.Min = evt.newValue;
            });
            
            _maxRollBoundField.RegisterValueChangedCallback(evt =>
            {
                Element.RollBounds.Max = evt.newValue;
            });
        }

        protected override void PostInitialize()
        {
            _minRollBoundField.value = Element.RollBounds.Min;
            _maxRollBoundField.value = Element.RollBounds.Max;
        }
    }
}