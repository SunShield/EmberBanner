using System;
using System.Linq;
using EmberBanner.Core.Enums.Actions;
using EmberBanner.Core.Enums.Battle.Targeting;
using UnityEngine.UIElements;

namespace EmberBanner.Editor.GameManagement.Tabs.Cards.Elements.Actions.TypedElements
{
    public class AggressionActionListElement : ActionListElement
    {
        protected override void PostInitialize()
        {
            base.PostInitialize();
            MagnitudeTypeDropdown.choices = Enum.GetValues(typeof(AggressionType)).Cast<AggressionType>().Select(e => e.ToString()).ToList();
            MagnitudeTypeDropdown.index = (int)Element.AggressionType;
            
            PossibleTargetsField.choices = Enum.GetValues(typeof(AggressionTargetType)).Cast<AggressionTargetType>().Select(e => e.ToString()).ToList();
            PossibleTargetsField.index = (int)Element.PossibleAggressionTargets;
        }
        
        protected override void PostAddEvents()
        {
            base.PostAddEvents();
            PossibleTargetsField.RegisterValueChangedCallback(evt =>
            {
                Element.PossibleAggressionTargets = PossibleTargetsField.index;
                FireOnPossibleTargetsChangedEvent();
            });
        }
    }
}