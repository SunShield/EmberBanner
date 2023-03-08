using EmberBanner.Core.Ingame.Impl.Battles;
using EmberBanner.Core.Ingame.Management.Factories;
using EmberBanner.Core.Models.Units.Crystals;
using EmberBanner.Core.Service.Debug;

namespace EmberBanner.Core.Entities.Management.Factories.Impl.Battles
{
    public class BattleUnitCrystalEntityFactory : EntityFactory<BattleUnitCrystalEntity, UnitCrystalModel>
    {
        private static BattleUnitCrystalEntityFactory _instance;
        public static BattleUnitCrystalEntityFactory I => _instance ??= new();

        protected override void OnPostCreateEntity(BattleUnitCrystalEntity entity, UnitCrystalModel model)
        {
            var message = $"Battle Unit Crystal Entity (id: {entity.Id} | roll: {entity.MinBound.CalculateValue()}~{entity.MaxBound.CalculateValue()}) created";
            
            EBDebugger.Log(EBDebugContext.Entities, EBDebugContext.Units, message);
        }
    }
}