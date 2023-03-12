using System;
using System.Linq;
using EmberBanner.Core.Enums.Actions;
using EmberBanner.Unity.Data.ScriptableObjects;
using UnityEngine.UIElements;

namespace EmberBanner.Editor.GameManagement.Tabs.Cards.Elements.Actions.TypedElements
{
    public class SupportActionListElement : ActionListElement
    {
        protected override void PostInitialize()
        {
            base.PostInitialize();
            MagnitudeTypeDropdown.choices = Enum.GetValues(typeof(SupportType)).Cast<SupportType>().Select(e => e.ToString()).ToList();
            MagnitudeTypeDropdown.index = (int)Element.SupportType;
            MagnitudeTypeSpriteElement.style.backgroundImage =
                new StyleBackground(GameData.EI.ActionTypeIcons[Element.SupportType.ToString()]);
        }
        
        protected override void PostAddEvents()
        {
            base.PostAddEvents();
            MagnitudeTypeDropdown.RegisterValueChangedCallback(evt =>
            {
                Element.SupportType = (SupportType)MagnitudeTypeDropdown.index;
                MagnitudeTypeSpriteElement.style.backgroundImage =
                    new StyleBackground(GameData.EI.ActionTypeIcons[Element.SupportType.ToString()]);
            });
        }
    }
}