using EmberBanner.Core.Ingame;
using EmberBanner.Core.Models.Units.Crystals;
using Plugins.ComplexValue;

namespace EmberBanner.Core.Entities.Impl.Units.Crystals
{
    public class UnitCrystalEntity : AbstractEntity<UnitCrystalModel>
    {
        public ComplexValue MinBound { get; private set; }
        public ComplexValue MaxBound { get; private set; }
        
        public UnitCrystalEntity(int id, UnitCrystalModel model) : base(id, model)
        {
            MinBound = new(true, model.RollBounds.Min);
            MaxBound = new(true, model.RollBounds.Max);
        }
    }
}