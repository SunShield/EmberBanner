using System;
using System.Linq;
using EmberBanner.Core.Enums;
using EmberBanner.Core.Enums.Actions;

namespace EmberBanner.Editor.GameManagement.Tabs.Cards.Elements.Actions.TypedElements
{
    public class DefenseActionListElement : ActionListElement
    {
        protected override void PostInitialize()
        {
            base.PostInitialize();
            MagnitudeTypeDropdown.choices = Enum.GetValues(typeof(DefenseType)).Cast<DefenseType>().Select(e => e.ToString()).ToList();
            MagnitudeTypeDropdown.index = (int)Element.DefenseType;
        }
    }
}