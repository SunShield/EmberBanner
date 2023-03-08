using EmberBanner.Core.Entities.Impl.Units.Crystals;
using EmberBanner.Core.Models.Units.Crystals;

namespace EmberBanner.Core.Ingame.Impl.Battles
{
    public class BattleUnitCrystalEntity : UnitCrystalEntity
    {
        public new BattleUnitEntity Owner => base.Owner as BattleUnitEntity;
        public int? CurrentRoll { get; private set; }
        
        public BattleUnitCrystalEntity(int id, UnitCrystalModel model) : base(id, model)
        {
        }

        public void DoRoll()
        {
            CurrentRoll = base.Roll();
        }
    }
}