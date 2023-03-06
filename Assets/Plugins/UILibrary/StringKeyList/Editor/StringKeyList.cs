using System;
using System.Linq;
using UILibrary.ManagedList.Editor;
using UnityEngine.UIElements;

namespace UILibrary.StringKeyList.Editor
{
    /// <summary>
    /// This class is a manipulated list of TInspectedValue elements
    ///
    /// The main restriction here is that every TInspectedValue should be strictly determined by a certain unique string key
    /// This list doesn't know where elements are placed, what they are etc.
    /// It tries ot be abstracted from everything related to elements themselves,
    /// And just gives an ability to manipulate elements list from somewhere
    /// </summary>
    /// <typeparam name="TInspectedValue"></typeparam>
    /// <typeparam name="TData"></typeparam>
    /// <typeparam name="TListElement"></typeparam>
    /// <typeparam name="TListElementData"></typeparam>
    public abstract class StringKeyList<TInspectedValue, TData, TListElement, TListElementData> : ManagedList<TInspectedValue, TData, TListElement, TListElementData>
        where TData : StringKeyListData<TInspectedValue>
        where TListElementData : ManagedListElementData
        where TListElement : ManagedListElement<TInspectedValue, TListElementData>
    {
        protected Predicate<TInspectedValue> PossibleElementCheckPredicate  { get; private set; }
        
        protected DropdownField PossibleValuesDropdown { get; private set; }
        public string CurrentValue => PossibleValuesDropdown.value;
        
        protected sealed override void PostGatherElements()
        {
            PossibleValuesDropdown = Root.Q<DropdownField>("ValuesDropdown");
        }

        protected sealed override void PostProcessData(TData data)
        {
            PossibleElementCheckPredicate = data.PossibleElementCheckPredicate;
        }

        protected override void PostAddElement(string elementKey) => UpdatePossibleValues();
        protected override void PostRemoveElement() => UpdatePossibleValues();
        protected override void PostUpdate() => UpdatePossibleValues();

        /// <summary>
        /// Updates list of possible values
        /// </summary>
        private void UpdatePossibleValues()
        {
            PossibleValuesDropdown.choices.Clear();
            
            var allElements = GetValuesPool();
            var possibleElements = allElements.Where(value => PossibleElementCheckPredicate(value));
            var elementKeys = possibleElements.Select(GetStringKey).ToList();
            PossibleValuesDropdown.choices = elementKeys;
            PossibleValuesDropdown.index = 0;
        }
    }
}