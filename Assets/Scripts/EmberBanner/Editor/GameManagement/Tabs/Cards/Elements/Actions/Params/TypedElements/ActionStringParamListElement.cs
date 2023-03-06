using UnityEngine.UIElements;

namespace EmberBanner.Editor.GameManagement.Tabs.Cards.Elements.Actions.Params.TypedElements
{
    public class ActionStringParamListElement : ActionParamListElement
    {
        protected override string UxmlKey { get; } = "ActionStringParamListElement";
        
        private TextField _valueField;

        protected override void PostGatherElements()
        {
            base.PostGatherElements();
            _valueField = Root.Q<TextField>("ValueField");
        }

        protected override void PostInitialize()
        {
            base.PostInitialize();
            _valueField.value = Element.StringValue;
        }

        protected override void PostAddEvents()
        {
            _valueField.RegisterValueChangedCallback(evt =>
            {
                Element.StringValue = evt.newValue;
            });
        }
    }
}