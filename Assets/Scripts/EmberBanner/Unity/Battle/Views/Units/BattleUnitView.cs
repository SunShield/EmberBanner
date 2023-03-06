using EmberBanner.Core.Enums.Battle;
using EmberBanner.Core.Ingame.Units;
using EmberBanner.Core.Models.Units;
using EmberBanner.Unity.Battle.Systems.CardZonesSystem;

namespace EmberBanner.Unity.Battle.Views.Units
{
    public class BattleUnitView : BattleView<UnitModel, UnitEntity>
    {
        private UnitCardZonesManager _zonesManager;
        
        public UnitControllerType Controller { get; private set; }
    }
}