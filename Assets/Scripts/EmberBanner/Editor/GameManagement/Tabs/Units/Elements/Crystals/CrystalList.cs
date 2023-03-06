using System.Linq;
using EmberBanner.Core.Models.Units.Crystals;
using UILibrary.ManagedList.Editor;

namespace EmberBanner.Editor.GameManagement.Tabs.Units.Elements.Crystals
{
    public class CrystalList : ManagedList<UnitCrystalModel, CrystalListData, CrystalListElement, CrystalListElementData>
    {
        protected override string UxmlKey { get; } = "CrystalList";
        
        private int _currentHighestKey;
        
        protected override string GetElementKey() => (_currentHighestKey + 1).ToString();
        protected override string GetStringKey(UnitCrystalModel value) => value.Name;
        protected override CrystalListElement CreateListElementInstance(UnitCrystalModel element) => new ();
        protected override CrystalListElementData CreateElementData(string elementKey) => new();

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