using EmberBanner.Core.Enums.Battle;
using EmberBanner.Core.Ingame.Impl.Battles;
using EmberBanner.Core.Models.Units.Crystals;

namespace EmberBanner.Unity.Battle.Views.Impl.Units.Crystals
{
    public class BattleUnitCrystalView : BattleView<BattleUnitCrystalEntity, UnitCrystalModel>
    {
        public int? CurrentRoll => Entity.CurrentRoll;
        public UnitControllerType Controller => Entity.Owner.Controller;
    }
}