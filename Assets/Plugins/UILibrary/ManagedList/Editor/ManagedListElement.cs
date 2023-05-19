using System;
using UILibrary.UxmlElement.Editor;
using UnityEngine.UIElements;

namespace UILibrary.ManagedList.Editor
{
    public abstract class ManagedListElement<TInspectedValue, TData> : VisualElementWithUxml
        where TData : ManagedListElementData
    {
        protected string ElementKey { get; private set; }
        
        /// <summary>
        /// List Element must know about real element to setup value bindings between them
        /// </summary>
        protected TInspectedValue Element { get; private set; } 
        
        protected Button RemoveButton { get; private set; }
        
        public void Initialize(string elementKey, TInspectedValue element, TData data)
        {
            ElementKey = elementKey;
            Element = element;
            ProcessData(data);
            GatherElements();
            AddEvents();
            PostInitialize();
        }
        
        protected virtual void ProcessData(TData data) { }
        
        private void GatherElements()
        {
            RemoveButton = Root.Q<Button>("RemoveButton");
            
            PostGatherElements();
        }
        
        protected virtual void PostGatherElements() { }

        private void AddEvents()
        {
            RemoveButton.clicked += FireOnRemoveClickedEvent;
            
            PostAddEvents();
        }

        private void FireOnRemoveClickedEvent() => onRemoveClicked?.Invoke(ElementKey);
        
        protected virtual void PostAddEvents() { }
        
        protected virtual void PostInitialize() { }

        // This must be called each time element is updated via editor
        protected void Update() => onUpdate?.Invoke(ElementKey);

        public event Action<string> onUpdate; 
        public event Action<string> onRemoveClicked;
    }
}