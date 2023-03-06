using EmberBanner.Core.Models.Units.Crystals;
using UILibrary.ManagedList.Editor;

namespace EmberBanner.Editor.GameManagement.Tabs.Units.Elements.Crystals
{
    public class CrystalListElement : ManagedListElement<UnitCrystalModel, CrystalListElementData>
    {
        protected override string UxmlKey { get; } = "CrystalListElement";
    }
}