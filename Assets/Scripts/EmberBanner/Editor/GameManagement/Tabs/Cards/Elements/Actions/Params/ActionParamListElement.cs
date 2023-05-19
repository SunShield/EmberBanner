using System;
using EmberBanner.Core.Enums;
using EmberBanner.Core.Enums.Actions;
using EmberBanner.Core.Models.Actions.Params;
using UILibrary.ManagedList.Editor;
using UnityEngine;
using UnityEngine.UIElements;

namespace EmberBanner.Editor.GameManagement.Tabs.Cards.Elements.Actions.Params
{
    public abstract class ActionParamListElement : ManagedListElement<ActionParamModel, ActionParamListElementData>
    {
        private VisualElement _colorTypeElement;
        private Label _nameLabel;
        private TextField _tagsField;

        protected override void PostGatherElements()
        {
            _nameLabel        = Root.Q<Label>("NameLabel");
            _colorTypeElement = Root.Q<VisualElement>("TypeColorElement");
            _tagsField        = Root.Q<TextField>("TagsField");
        }

        protected override void PostInitialize()
        {
            _colorTypeElement.style.backgroundColor = new StyleColor(GetTypeColor());
            _tagsField.value = Element.TagString;
            _nameLabel.text = Element.Name;
        }

        protected override void PostAddEvents()
        {
            _tagsField.RegisterValueChangedCallback(evt =>
            {
                Element.TagString = evt.newValue;
                Update();
            });
        }

        private Color GetTypeColor() => Element.Type switch
        {
            ActionParamType.Int    => new Color(0.8f, 0.5f, 0.3f, 1f),
            ActionParamType.String => new Color(0.3f, 0.4f, 0.9f, 1f),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}