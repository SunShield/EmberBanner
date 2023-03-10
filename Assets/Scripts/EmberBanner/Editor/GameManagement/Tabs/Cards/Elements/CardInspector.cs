using System;
using System.Linq;
using EmberBanner.Core.Enums.Actions;
using EmberBanner.Core.Enums.Battle;
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
        private DropdownField _mainTargetDropdown;

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
            _mainTargetDropdown = Root.Q<DropdownField>("MainTargetDropdown");
            
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

                if (InspectedElement.Actions.Count == 1)
                    action.IsMain = true;
                
                UpdateMainTargetDropdown();
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

                if (InspectedElement.Actions.Count == 0)
                    InspectedElement.MainTarget = CardMainTargetType.No;
                
                UpdateMainTargetDropdown();
                Database.Update();
            }
            
            _actionList = new ActionList();
            _actionList.Initialize(actionListData);
            _actionList.onMainActionSet += OnMainActionChanged;
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

            _mainTargetDropdown.RegisterValueChangedCallback(evt =>
            {
                InspectedElement.MainTarget = Enum.Parse<CardMainTargetType>(evt.newValue);
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
            UpdateMainTargetDropdown();
        }

        private void UpdateMainTargetDropdown()
        {
            _mainTargetDropdown.choices.Clear();
            _mainTargetDropdown.choices.Add("No");

            var mainAction = InspectedElement.Actions.FirstOrDefault(a => a.IsMain);

            if (mainAction == null) return;
            
            if (mainAction.Type == ActionType.Aggression)
            {
                _mainTargetDropdown.choices.Add("Enemy");
            }
            else if (mainAction.Type == ActionType.Defense)
            {
                _mainTargetDropdown.choices.Add("Ally");
                _mainTargetDropdown.choices.Add("Enemy");
            }
            else if (mainAction.Type == ActionType.Support)
            {
                _mainTargetDropdown.choices.Add("Ally");
            }

            _mainTargetDropdown.index = _mainTargetDropdown.choices.IndexOf(InspectedElement.MainTarget.ToString());
        }

        private void OnMainActionChanged(string mainActionName)
        {
            foreach (var action in InspectedElement.Actions)
            {
                action.IsMain = action.Name == mainActionName;
            }

            InspectedElement.MainTarget = CardMainTargetType.No;

            UpdateMainTargetDropdown();
        }
    }
}