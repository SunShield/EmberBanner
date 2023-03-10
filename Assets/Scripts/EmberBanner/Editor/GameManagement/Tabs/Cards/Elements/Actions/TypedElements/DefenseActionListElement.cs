using System;
using System.Linq;
using EmberBanner.Core.Enums.Actions;
using EmberBanner.Core.Enums.Battle.Targeting;
using UnityEngine.UIElements;

namespace EmberBanner.Editor.GameManagement.Tabs.Cards.Elements.Actions.TypedElements
{
    public class DefenseActionListElement : ActionListElement
    {
        protected override void PostInitialize()
        {
            base.PostInitialize();
            MagnitudeTypeDropdown.choices = Enum.GetValues(typeof(DefenseType)).Cast<DefenseType>().Select(e => e.ToString()).ToList();
            MagnitudeTypeDropdown.index = (int)Element.DefenseType;
            
            PossibleTargetsField.choices = Enum.GetValues(typeof(DefenseTargetType)).Cast<DefenseTargetType>().Select(e => e.ToString()).ToList();
            PossibleTargetsField.index = (int)Element.PossibleDefenseTargets;
        }
        
        protected override void PostAddEvents()
        {
            base.PostAddEvents();
            PossibleTargetsField.RegisterValueChangedCallback(evt =>
            {
                Element.PossibleDefenseTargets = PossibleTargetsField.index;
                FireOnPossibleTargetsChangedEvent();
            });
        }
    }
}