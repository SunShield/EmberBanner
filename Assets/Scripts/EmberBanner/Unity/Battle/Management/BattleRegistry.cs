using System.Collections.Generic;
using EmberBanner.Core.Enums.Battle;
using EmberBanner.Unity.Battle.Views.Cards;
using EmberBanner.Unity.Battle.Views.Units;

namespace EmberBanner.Unity.Battle.Management
{
    public class BattleRegistry
    {
        public Dictionary<int, BattleCardView> Cards       { get; private set; } = new();
        public Dictionary<int, BattleUnitView> Units       { get; private set; } = new();
        public Dictionary<int, BattleUnitView> PlayerUnits { get; private set; } = new();
        public Dictionary<int, BattleUnitView> EnemyUnits  { get; private set; } = new();

        public void AddCard(BattleCardView card) => Cards.Add(card.Id, card);
        
        public void AddUnit(BattleUnitView unit)
        {
            Units.Add(unit.Id, unit);
            
            if (unit.Controller == UnitControllerType.Player) PlayerUnits.Add(unit.Id, unit);
            else                                              EnemyUnits.Add(unit.Id, unit);
        }
    }
}