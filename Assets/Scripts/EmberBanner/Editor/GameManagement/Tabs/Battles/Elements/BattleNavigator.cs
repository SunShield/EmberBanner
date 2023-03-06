using EmberBanner.Core.Models.Battles;
using EmberBanner.Unity.Data.ScriptableObjects.Databases;
using NFate.Editor.EditorElements;

namespace EmberBanner.Editor.GameManagement.Tabs.Battles.Elements
{
    public class BattleNavigator : DictionaryNavigatorWithUxml<string, BattleModel, BattleNavigatorElement, BattlesDatabase>
    {
        protected override string UxmlKey { get; } = "BattleNavigator";
    }
}