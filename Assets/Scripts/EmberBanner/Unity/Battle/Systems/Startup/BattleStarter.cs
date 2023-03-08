using EmberBanner.Core.Entities.Management.Factories.Impl.Battles;
using EmberBanner.Core.Ingame.Impl.Battles;
using EmberBanner.Core.Service.Debug;
using EmberBanner.Unity.Battle.Systems.Visuals;
using EmberBanner.Unity.Service;

namespace EmberBanner.Unity.Battle.Systems.Startup
{
    public class BattleStarter : EBMonoBehaviour
    {
        public void Start()
        {
            var battle = BattleEntityFactory.I.CreateEntity("Test Battle");
            StartBattle(battle);
        }

        private void StartBattle(BattleEntity battle)
        {
            EBDebugger.Log(EBDebugContext.Battle, "Starting battle");
            BattleVisualsManager.I.SetVisuals(battle);
        }
    }
}