using EmberBanner.Core.Models.Units;
using EmberBanner.Unity.Data.ScriptableObjects.Databases;
using NFate.Editor.EditorElements;

namespace EmberBanner.Editor.GameManagement.Tabs.Units.Elements
{
    public class UnitNavigator : DictionaryNavigatorWithUxml<string, UnitModel, UnitNavigatorElement, UnitsDatabase>
    {
        protected override string UxmlKey { get; } = "UnitNavigator";
    }
}