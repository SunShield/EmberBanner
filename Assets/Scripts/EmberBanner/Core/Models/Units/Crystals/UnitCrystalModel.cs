using System;
using EmberBanner.Core.Service.Classes.Fundamental;

namespace EmberBanner.Core.Models.Units.Crystals
{
    [Serializable]
    public class UnitCrystalModel : AbstractModel
    {
        public IntSpan RollBounds = new();
    }
}