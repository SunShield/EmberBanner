using System.Linq;
using EmberBanner.Core.Models.Battles;
using UILibrary.ManagedList.Editor;

namespace EmberBanner.Editor.GameManagement.Tabs.Battles.Elements.BattleUnits
{
    public class BattleUnitList : ManagedList<UnitInBattleModel, BattleUnitListData, BattleUnitListElement, BattleUnitListElementData>
    {
        protected override string UxmlKey { get; } = "BattleUnitList";
        
        private int _currentHighestKey;
        
        protected override string GetElementKey() => (_currentHighestKey + 1).ToString();
        protected override string GetStringKey(UnitInBattleModel value) => value.Name;
        protected override BattleUnitListElement CreateListElementInstance(UnitInBattleModel element) => new ();
        protected override BattleUnitListElementData CreateElementData(string elementKey) => new();

        protected override void PostAddElement(string elementKey)
        {
            _currentHighestKey++;
        }

        protected override void PostUpdate()
        {
            var elements = GetValuesPool();
            _currentHighestKey = elements.Count != 0 
                ? elements.Select(e => int.Parse(e.Name)).Max() 
                : 0;
        }
    }
}