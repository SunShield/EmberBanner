﻿using System;
using EmberBanner.Core.Models.Units;
using EmberBanner.Editor.GameManagement.Tabs.Units.Elements;
using EmberBanner.Unity.Data.ScriptableObjects.Databases;
using ItemsManager.Editor.Tabs;
using TabbedWindow.Windows;

namespace EmberBanner.Editor.GameManagement.Tabs.Units
{
    public class UnitsManagementTab : DictionaryDatabaseManagerTab<string, UnitModel, UnitNavigatorElement, UnitsDatabase, GeneralDatabase, UnitInspector, UnitNavigator>
    {
        public UnitsManagementTab(AbstractWindow window, string name, string assetLocation) : base(window, name, assetLocation)
        {
        }

        protected override UnitsDatabase GetDatabase() => GeneralDatabase.Units;

        public void OnCardAdded(string cardName)
        {
            Inspector.Update();
        }

        public void OnCardRemoved(string cardName)
        {
            foreach (var unitModel in Database.Elements.Values)
            {
                foreach (var defaultCardModel in unitModel.DefaultCards)
                {
                    if (defaultCardModel.CardName != cardName) continue;
                    defaultCardModel.CardName = "";
                }
            }
        }

        protected override void PostPrepare()
        {
            Navigator.onElementAdded += FireOnUnitAddedEvent;
            Navigator.onElementRemoved += unit => FireOnUnitRemovedEvent(unit.Name);
        }
        
        private void FireOnUnitAddedEvent(string unitName) => onUnitAdded?.Invoke(unitName);
        private void FireOnUnitRemovedEvent(string unitName) => onUnitRemoved?.Invoke(unitName);
        
        public event Action<string> onUnitAdded; 
        public event Action<string> onUnitRemoved;
    }
}