using System;
using System.Linq;
using EmberBanner.Core.Enums.Battle.Targeting;
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
        private VisualElement _baseStatsContainer;
        private DropdownField _possibleTargetsField;

        public CardInspector() : base()
        {
            BuildGeometry();
            AddEvents();
        }

        private void BuildGeometry()
        {
            style.flexGrow = 1f;
            
            _elementName = Root.Q<Label>("ElementName");
            _icon = Root.Q<VisualElement>("Icon");
            _iconPicker = Root.Q<ObjectField>("IconPicker");
            _costField = Root.Q<IntegerField>("CostField");
            _actionsContainer = Root.Q<VisualElement>("ActionsContainer");
            _baseStatsContainer = Root.Q<VisualElement>("BaseStatsContainer");
            _possibleTargetsField = new DropdownField();
            _possibleTargetsField.choices.AddRange(Enum.GetValues(typeof(TargetType)).Cast<TargetType>().Select(e => e.ToString()).ToList());
            _baseStatsContainer.Add(_possibleTargetsField);
            
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
                var action = new ActionModel(actionName, _actionList.CurrentActionType)
                {
                    PossibleTargets = 0,
                    WielderCardName = InspectedElement.Name
                };
                
                InspectedElement.Actions.Add(action);
                
                UpdatePossibleTargetsDropdown();
                Database.Update();
            }

            void RemoveAction(string actionName)
            {
                for (int i = 0; i < InspectedElement.Actions.Count; i++)
                {
                    if (InspectedElement.Actions[i].Name == actionName)
                    {
                        InspectedElement.Actions.RemoveAt(i);
                        break;
                    }
                }
                
                UpdatePossibleTargetsDropdown();
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

            _possibleTargetsField.RegisterValueChangedCallback(evt =>
            {
                InspectedElement.Target = _possibleTargetsField.index;
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
            _possibleTargetsField.index = (int)InspectedElement.Target;
            _actionList.Update();
            UpdatePossibleTargetsDropdown();
        }

        private void UpdatePossibleTargetsDropdown()
        {
        }
    }
}