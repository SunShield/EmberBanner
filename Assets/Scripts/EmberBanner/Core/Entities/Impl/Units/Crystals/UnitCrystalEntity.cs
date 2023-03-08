using EmberBanner.Core.Ingame;
using EmberBanner.Core.Models.Units.Crystals;
using Plugins.ComplexValue;
using UnityEngine;

namespace EmberBanner.Core.Entities.Impl.Units.Crystals
{
    public class UnitCrystalEntity : AbstractEntity<UnitCrystalModel>
    {
        public UnitEntity Owner { get; private set; }
        public ComplexValue MinBound { get; private set; }
        public ComplexValue MaxBound { get; private set; }
        
        public UnitCrystalEntity(int id, UnitCrystalModel model) : base(id, model)
        {
            MinBound = new(true, model.RollBounds.Min);
            MaxBound = new(true, model.RollBounds.Max);
        }

        public override void Initialize(object payload)
        {
            Owner = payload as UnitEntity;
        }

        public int Roll() => Random.Range(MinBound.CalculateValue(), MaxBound.CalculateValue() + 1);
    }
}