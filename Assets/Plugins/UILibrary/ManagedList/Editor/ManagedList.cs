using System;
using System.Collections.Generic;
using System.Linq;
using UILibrary.UxmlElement.Editor;
using UnityEngine.UIElements;

namespace UILibrary.ManagedList.Editor
{
    /// <summary>
    /// This class is a List Visual Element, able to add and remove elements
    /// </summary>
    /// <typeparam name="TInspectedValue"></typeparam>
    /// <typeparam name="TData"></typeparam>
    /// <typeparam name="TListElement"></typeparam>
    /// <typeparam name="TListElementData"></typeparam>
    public abstract class ManagedList <TInspectedValue, TData, TListElement, TListElementData> : VisualElementWithUxml
        where TData : ManagedListData<TInspectedValue>
        where TListElementData : ManagedListElementData
        where TListElement : ManagedListElement<TInspectedValue, TListElementData>
    {
        protected Dictionary<string, TListElement> Elements { get; private set; } = new();
        
        protected Predicate<TInspectedValue>    ElementInListPredicate         { get; private set; }
        protected Action<string>                OnAddElementClickedCallback    { get; private set; }
        protected Action<string>                OnRemoveElementClickedCallback { get; private set; }
        protected Func<List<TInspectedValue>>   ValuesPoolGetter               { get; private set; }
        protected Func<string, TInspectedValue> ElementByKeyGetter             { get; private set; }

        protected Button        AddButton              { get; private set; }
        protected ScrollView    ElementsContainer      { get; private set; }
        
        public void Initialize(TData data)
        {
            ProcessData(data);
            GatherElements();
            AddEvents();
        }

        private void GatherElements()
        {
            AddButton              = Root.Q<Button>("AddButton");
            ElementsContainer      = Root.Q<ScrollView>("ElementsContainer");

            PostGatherElements();
        }
        
        protected virtual void PostGatherElements() { }

        private void ProcessData(TData data)
        {
            ElementInListPredicate = data.ElementInListPredicate;
            OnAddElementClickedCallback = data.OnAddElementClickedCallback;
            OnRemoveElementClickedCallback = data.OnRemoveElementClickedCallback;
            ValuesPoolGetter = data.ValuesPoolGetter;
            ElementByKeyGetter = data.ElementByKeyGetter;

            PostProcessData(data);
        }
        
        protected virtual void PostProcessData(TData data) { }

        private void AddEvents()
        {
            AddButton.clicked += () =>
            {
                var elementKey = GetElementKey();
                if (string.IsNullOrEmpty(elementKey)) return;
                AddElement(elementKey);
            };
            PostAddEvents();
        }

        protected abstract string GetElementKey();
        
        protected virtual void PostAddEvents() { }

        /// <summary>
        /// Adds visuals for elements already in list
        /// </summary>
        private void AddCurrentElements()
        {
            var allValues = GetValuesPool();
            var currentValues = allValues.Where(value => ElementInListPredicate(value));
            foreach (var currentValue in currentValues)
            {
                var valueKey = GetStringKey(currentValue);
                AddListElement(valueKey);
            }
        }

        /// <summary>
        /// All theoretically possible values, from which the currently possible ones should be filtered 
        /// </summary>
        /// <returns></returns>
        protected List<TInspectedValue> GetValuesPool() => ValuesPoolGetter();

        /// <summary>
        /// Getting key of a certain value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected abstract string GetStringKey(TInspectedValue value);

        private void AddElement(string elementKey)
        {
            OnAddElementClickedCallback?.Invoke(elementKey);
            AddListElement(elementKey);
            PostAddElement(elementKey);
        }

        protected TInspectedValue GetElementByKey(string elementKey) => ElementByKeyGetter(elementKey);

        private void AddListElement(string elementKey)
        {
            var realElement = GetElementByKey(elementKey);
            var listElement = CreateListElement(elementKey, realElement);

            Elements.Add(elementKey, listElement);
            ElementsContainer.Add(listElement);
        }

        protected virtual void PostAddElement(string elementKey) { }

        private TListElement CreateListElement(string elementKey, TInspectedValue realElement)
        {
            var listElement = CreateListElementInstance(realElement);
            var elementData = CreateElementData(elementKey);
            listElement.Initialize(elementKey, realElement, elementData);
            listElement.onRemoveClicked += RemoveElement;
            PostAddListElement(elementKey, listElement);
            
            return listElement;
        }

        protected abstract TListElement CreateListElementInstance(TInspectedValue element);
        protected abstract TListElementData CreateElementData(string elementKey);
        protected virtual void PostAddListElement(string elementKey, TListElement listElement) { }

        private void RemoveElement(string elementKey)
        {
            RemoveListElement(elementKey);
            OnRemoveElementClickedCallback?.Invoke(elementKey);
            PostRemoveElement();
        }
        
        protected virtual void PostRemoveElement() { }

        private void RemoveListElement(string elementKey)
        {
            var listElement = Elements[elementKey];
            ElementsContainer.Remove(listElement);
            Elements.Remove(elementKey);
        }
        
        public void Update()
        {
            ClearElements();
            AddCurrentElements();
            PostUpdate();
        }
        
        protected virtual void PostUpdate() { }

        private void ClearElements()
        {
            foreach (var element in Elements.Values)
            {
                ElementsContainer.Remove(element);
            }
            
            Elements.Clear();
        }
    }
}