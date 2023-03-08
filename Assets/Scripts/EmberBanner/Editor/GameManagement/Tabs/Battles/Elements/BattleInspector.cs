using System.Linq;
using EmberBanner.Core.Models.Battles;
using EmberBanner.Editor.GameManagement.Tabs.Battles.Elements.BattleUnits;
using EmberBanner.Unity.Data.ScriptableObjects.Databases;
using NFate.Editor.EditorElements;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace EmberBanner.Editor.GameManagement.Tabs.Battles.Elements
{
    public class BattleInspector : InspectorWithUxml<BattleModel, BattlesDatabase>
    {
        protected override string UxmlKey { get; } = "BattleInspector";
        
        private Label          _elementName;
        private VisualElement  _icon;
        private ObjectField    _iconPicker;
        private VisualElement  _unitsContainer;
        private BattleUnitList _battleUnitList;

        public BattleInspector() : base()
        {
            BuildGeometry();
            AddEvents();
        }

        private void BuildGeometry()
        {
            style.flexGrow = 1f;
            
            _elementName    = Root.Q<Label>("ElementName");
            _icon           = Root.Q<VisualElement>("Icon");
            _iconPicker     = Root.Q<ObjectField>("IconPicker");
            _unitsContainer = Root.Q<VisualElement>("UnitsContainer");

            AddBattleUnitList();
        }

        private void AddBattleUnitList()
        {
            var data = new BattleUnitListData()
            {
                ValuesPoolGetter = () => InspectedElement.DeterminedEnemies,
                ElementByKeyGetter = key => InspectedElement.DeterminedEnemies.First(enemy => enemy.Name == key),
                ElementInListPredicate = crystal => true,
                OnAddElementClickedCallback = AddUnit,
                OnRemoveElementClickedCallback = RemoveUnit
            };

            void AddUnit(string crystalName)
            {
                InspectedElement.DeterminedEnemies.Add(new UnitInBattleModel(crystalName) { Wave = 1 });
                Database.Update();
            }

            void RemoveUnit(string crystalName)
            {
                for (int i = 0; i < InspectedElement.DeterminedEnemies.Count; i++)
                {
                    if (InspectedElement.DeterminedEnemies[i].Name == crystalName)
                    {
                        InspectedElement.DeterminedEnemies.RemoveAt(i);
                        return;
                    }
                }
                Database.Update();
            }
            
            _battleUnitList = new BattleUnitList();
            _battleUnitList.Initialize(data);
            _unitsContainer.Add(_battleUnitList);
        }

        private void AddEvents()
        {
            _iconPicker.RegisterValueChangedCallback(evt =>
            {
                InspectedElement.Sprite = evt.newValue as Sprite;
                _icon.style.backgroundImage = new StyleBackground(InspectedElement.Sprite);
                PostElementUpdate();
            });
        }
        
        protected override void PostPrepare()
        {
        }

        protected override void OnElementSet()
        {
            Update();
        }

        public void Update()
        {
            _elementName.text = InspectedElement.Name;
            _icon.style.backgroundImage = new StyleBackground(InspectedElement.Sprite);
            _iconPicker.value = InspectedElement.Sprite;
            
            _battleUnitList.Update();
        }
    }
}