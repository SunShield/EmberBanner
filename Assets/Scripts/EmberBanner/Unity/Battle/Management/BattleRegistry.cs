using System.Collections.Generic;
using EmberBanner.Core.Enums.Battle;
using EmberBanner.Unity.Battle.Views.Impl.Cards;
using EmberBanner.Unity.Battle.Views.Impl.Units;

namespace EmberBanner.Unity.Battle.Management
{
    /// <summary>
    /// This class holds only currently "active" stuff: units and cards which are directly in battle right now 
    /// </summary>
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
        
        public void RemoveUnit(BattleUnitView unit)
        {
            
            if (unit.Controller == UnitControllerType.Player) PlayerUnits.Remove(unit.Id);
            else                                              EnemyUnits.Remove(unit.Id);
            
            Units.Remove(unit.Id);                                         
        }
    }
}