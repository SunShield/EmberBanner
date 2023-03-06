using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace EmberBanner.Editor.GameManagement.Tabs.Cards.Elements.Actions.Params.TypedElements
{
    public class ActionIntParamListElement : ActionParamListElement
    {
        protected override string UxmlKey { get; } = "ActionIntParamListElement";

        private IntegerField _valueField;

        protected override void PostGatherElements()
        {
            base.PostGatherElements();
            _valueField = Root.Q<IntegerField>("ValueField");
        }

        protected override void PostInitialize()
        {
            base.PostInitialize();
            _valueField.value = Element.IntValue;
        }

        protected override void PostAddEvents()
        {
            _valueField.RegisterValueChangedCallback(evt =>
            {
                Element.IntValue = evt.newValue;
            });
        }
    }
}