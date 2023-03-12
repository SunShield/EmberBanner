using System;
using System.Linq;
using EmberBanner.Core.Enums.Actions;
using EmberBanner.Unity.Data.ScriptableObjects;
using UnityEngine.UIElements;

namespace EmberBanner.Editor.GameManagement.Tabs.Cards.Elements.Actions.TypedElements
{
    public class AggressionActionListElement : ActionListElement
    {
        protected override void PostInitialize()
        {
            base.PostInitialize();
            MagnitudeTypeDropdown.choices = Enum.GetValues(typeof(AggressionType)).Cast<AggressionType>().Select(e => e.ToString()).ToList();
            MagnitudeTypeSpriteElement.style.backgroundImage =
                new StyleBackground(GameData.EI.ActionTypeIcons[Element.AggressionType.ToString()]);
            MagnitudeTypeDropdown.index = (int)Element.AggressionType;
        }

        protected override void PostAddEvents()
        {
            base.PostAddEvents();
            MagnitudeTypeDropdown.RegisterValueChangedCallback(evt =>
            {
                Element.AggressionType = (AggressionType)MagnitudeTypeDropdown.index;
                MagnitudeTypeSpriteElement.style.backgroundImage =
                    new StyleBackground(GameData.EI.ActionTypeIcons[Element.AggressionType.ToString()]);
            });
        }
    }
}