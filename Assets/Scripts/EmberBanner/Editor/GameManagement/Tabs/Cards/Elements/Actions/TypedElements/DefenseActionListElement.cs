using System;
using System.Linq;
using EmberBanner.Core.Enums.Actions;
using EmberBanner.Unity.Data.ScriptableObjects;
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
            MagnitudeTypeSpriteElement.style.backgroundImage =
                new StyleBackground(GameData.EI.ActionTypeIcons[Element.DefenseType.ToString()]);
        }
        
        protected override void PostAddEvents()
        {
            base.PostAddEvents();
            MagnitudeTypeDropdown.RegisterValueChangedCallback(evt =>
            {
                Element.DefenseType = (DefenseType)MagnitudeTypeDropdown.index;
                MagnitudeTypeSpriteElement.style.backgroundImage =
                    new StyleBackground(GameData.EI.ActionTypeIcons[Element.DefenseType.ToString()]);
            });
        }
    }
}