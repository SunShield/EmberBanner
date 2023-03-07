using System.Linq;
using EmberBanner.Core.Models.Units.Cards;
using UILibrary.ManagedList.Editor;

namespace EmberBanner.Editor.GameManagement.Tabs.Units.Elements.Cards
{
    public class UnitCardList : ManagedList<UnitDefaultCardModel, UnitCardListData, UnitCardListElement, UnitCardListElementData>
    {
        protected override string UxmlKey { get; } = "UnitCardList";
        
        private int _currentHighestKey;
        
        protected override string GetElementKey() => (_currentHighestKey + 1).ToString();
        protected override string GetStringKey(UnitDefaultCardModel value) => value.Name;
        protected override UnitCardListElement CreateListElementInstance(UnitDefaultCardModel element) => new ();
        protected override UnitCardListElementData CreateElementData(string elementKey) => new();

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