using System;
using System.Linq;
using EmberBanner.Core.Enums.Battle.Targeting;
using UnityEngine.UIElements;

namespace EmberBanner.Editor.GameManagement.Tabs.Cards.Elements.Actions.TypedElements
{
    public class SupportActionListElement : ActionListElement
    {
        protected override void PostInitialize()
        {
            base.PostInitialize();
            MagnitudeTypeDropdown.parent.Remove(MagnitudeTypeDropdown);
            
            PossibleTargetsField.choices = Enum.GetValues(typeof(SupportTargetType)).Cast<SupportTargetType>().Select(e => e.ToString()).ToList();
            PossibleTargetsField.index = (int)Element.PossibleDefenseTargets;
        }

        protected override void PostAddEvents()
        {
            base.PostAddEvents();
            PossibleTargetsField.RegisterValueChangedCallback(evt =>
            {
                Element.PossibleSupportTargets = PossibleTargetsField.index;
                FireOnPossibleTargetsChangedEvent();
            });
        }
    }
}