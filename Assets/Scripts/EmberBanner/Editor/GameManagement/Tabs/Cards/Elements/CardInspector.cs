using System.Linq;
using EmberBanner.Core.Models.Actions;
using EmberBanner.Core.Models.Cards;
using EmberBanner.Editor.GameManagement.Tabs.Cards.Elements.Actions;
using EmberBanner.Unity.Data.ScriptableObjects.Databases;
using NFate.Editor.EditorElements;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace EmberBanner.Editor.GameManagement.Tabs.Cards.Elements
{
    public class CardInspector : InspectorWithUxml<CardModel, CardsDatabase>
    {
        protected override string UxmlKey { get; } = "CardInspector";
        
        private Label         _elementName;
        private VisualElement _icon;
        private ObjectField   _iconPicker;
        private IntegerField  _costField;
        private VisualElement _actionsContainer;
        private ActionList    _actionList;

        public CardInspector() : base()
        {
            BuildGeometry();
            AddEvents();
        }

        private void BuildGeometry()
        {
            _elementName = Root.Q<Label>("ElementName");
            _icon = Root.Q<VisualElement>("Icon");
            _iconPicker = Root.Q<ObjectField>("IconPicker");
            _costField = Root.Q<IntegerField>("CostField");
            _actionsContainer = Root.Q<VisualElement>("ActionsContainer");

            AddActionsList();
        }

        private void AddActionsList()
        {
            var actionListData = new ActionListData()
            {
                ValuesPoolGetter = () => InspectedElement.Actions,
                ElementByKeyGetter = n => InspectedElement.Actions.First(a => a.Name == n),
                ElementInListPredicate = (e) => true,
                OnAddElementClickedCallback = AddAction,
                OnRemoveElementClickedCallback = RemoveAction
            };

            void AddAction(string actionName)
            {
                var action = new ActionModel(actionName, _actionList.CurrentActionType);
                
                InspectedElement.Actions.Add(action);
                Database.Update();
            }

            void RemoveAction(string actionName)
            {
                for (int i = 0; i < InspectedElement.Actions.Count; i++)
                {
                    if (InspectedElement.Actions[i].Name == actionName)
                    {
                        InspectedElement.Actions.RemoveAt(i);
                        return;
                    }
                }
                Database.Update();
            }
            
            _actionList = new ActionList();
            _actionList.Initialize(actionListData);
            _actionsContainer.Add(_actionList);
        }

        private void AddEvents()
        {
            _iconPicker.RegisterValueChangedCallback(evt =>
            {
                InspectedElement.Sprite = evt.newValue as Sprite;
                _icon.style.backgroundImage = new StyleBackground(InspectedElement.Sprite);
                PostElementUpdate();
            });

            _costField.RegisterValueChangedCallback(evt =>
            {
                InspectedElement.Cost = evt.newValue;
            });
        }
        
        protected override void PostPrepare()
        {
        }

        protected override void OnElementSet()
        {
            _elementName.text = InspectedElement.Name;
            _icon.style.backgroundImage = new StyleBackground(InspectedElement.Sprite);
            _iconPicker.value = InspectedElement.Sprite;
            _costField.value = InspectedElement.Cost;
            _actionList.Update();
        }
    }
}