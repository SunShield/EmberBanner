using EmberBanner.Core.Ingame.Impl.Battles;
using EmberBanner.Unity.Battle.Management;
using EmberBanner.Unity.Battle.Systems.Visuals.ActionsResolve;
using EmberBanner.Unity.Battle.Systems.Visuals.Arrows;

namespace EmberBanner.Unity.Battle.Systems.DeathHandling
{
    public class UnitDeathHandler
    {
        private static UnitDeathHandler _instance;
        public static UnitDeathHandler I => _instance ??= new();

        public void HandleUnitDeath(BattleUnitEntity unit)
        {
            var view = BattleManager.I.Registry.Units[unit.Id];
            
            foreach (var crystal in view.UnitCrystals.Crystals)
            {
                ActionsResolveUi.I.ClearCrystalIfNeeded(crystal);
            }
            
            view.Spot.RemoveUnit();
            view.gameObject.SetActive(false);
            CardTargetsMatrixUi.I.RedrawArrows();
        }
    }
}