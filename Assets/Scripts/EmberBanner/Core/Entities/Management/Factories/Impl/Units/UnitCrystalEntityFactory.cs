using EmberBanner.Core.Entities.Impl.Units.Crystals;
using EmberBanner.Core.Ingame.Management.Factories;
using EmberBanner.Core.Models.Units.Crystals;
using EmberBanner.Core.Service.Debug;

namespace EmberBanner.Core.Entities.Management.Factories.Impl.Units
{
    public class UnitCrystalEntityFactory : EntityFactory<UnitCrystalEntity, UnitCrystalModel>
    {
        private static UnitCrystalEntityFactory _instance;
        public static UnitCrystalEntityFactory I => _instance ??= new();

        protected override void OnPostCreateEntity(UnitCrystalEntity entity, UnitCrystalModel model)
        {
            var message = $"Unit Crystal Entity (id: {entity.Id} | roll: {entity.MinBound.CalculateValue()}~{entity.MaxBound.CalculateValue()}) created";
            var tempMessage = "Temporary ";
            var finalMessage = NextEntityIsTemporary ? tempMessage : "";
            finalMessage += message;
            
            EBDebugger.Log(EBDebugContext.Entities, EBDebugContext.Units, finalMessage);
        }
    }
}