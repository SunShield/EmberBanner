using System;
using System.Linq;
using EmberBanner.Core.Enums;
using EmberBanner.Core.Enums.Actions;
using EmberBanner.Core.Models.Actions.Params;
using EmberBanner.Editor.GameManagement.Tabs.Cards.Elements.Actions.Params.TypedElements;
using UILibrary.ManagedList.Editor;
using UnityEngine.UIElements;

namespace EmberBanner.Editor.GameManagement.Tabs.Cards.Elements.Actions.Params
{
    public class ActionParamList : ManagedList<ActionParamModel, ActionParamListData, ActionParamListElement, ActionParamListElementData>
    {
        protected override string UxmlKey { get; } = "ActionParamList";
        protected override string GetElementKey() => _newParamNameField.value;
        protected override string GetStringKey(ActionParamModel value) => value.Name;

        private TextField _newParamNameField;
        private DropdownField _paramTypesDropdown;
        public string CurrentParamType => _paramTypesDropdown.value;

        protected override void PostGatherElements()
        {
            _newParamNameField = Root.Q<TextField>("ParamNameField");
            
            _paramTypesDropdown = Root.Q<DropdownField>("ActionParamType");
            _paramTypesDropdown.choices = Enum.GetValues(typeof(ActionParamType)).Cast<ActionParamType>().Select(e => e.ToString()).ToList();
            _paramTypesDropdown.index = 0;
        }

        protected override ActionParamListElement CreateListElementInstance(ActionParamModel element) => element.Type switch
        {
            ActionParamType.Int    => new ActionIntParamListElement(),
            ActionParamType.String => new ActionStringParamListElement(),
            _ => throw new ArgumentOutOfRangeException()
        };

        protected override ActionParamListElementData CreateElementData(string elementKey) => new();
    }
}