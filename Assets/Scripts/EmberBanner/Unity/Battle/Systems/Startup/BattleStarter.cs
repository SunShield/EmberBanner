using EmberBanner.Core.Entities.Management.Factories.Impl.Battles;
using EmberBanner.Core.Enums.Battle;
using EmberBanner.Core.Ingame.Impl.Battles;
using EmberBanner.Core.Service.Debug;
using EmberBanner.Unity.Battle.Management;
using EmberBanner.Unity.Battle.Systems.Visuals;
using EmberBanner.Unity.Battle.Views.Factories.Impl;
using EmberBanner.Unity.Service;

namespace EmberBanner.Unity.Battle.Systems.Startup
{
    public class BattleStarter : EBMonoBehaviour
    {
        public void Start()
        {
            var battle = BattleEntityFactory.I.CreateEntity("Test Battle");
            AddTestHeroes(battle);
            BattleManager.I.InitializeBattle(battle);
            StartBattle(battle);
        }

        private void StartBattle(BattleEntity battle)
        {
            EBDebugger.Log(EBDebugContext.Battle, "Starting battle");
            BattleVisualsManager.I.SetVisuals(battle);
            BuildViews(battle);
        }

        // Later heroes will be taken from persistent storage
        private void AddTestHeroes(BattleEntity entity)
        {
            var hero = BattleUnitEntityFactory.I.CreateEntity("Sir Calvus", UnitControllerType.Player);
            entity.Heroes.Add(hero);
        }

        private void BuildViews(BattleEntity entity)
        {
            foreach (var hero in entity.Heroes)
            {
                BattleUnitViewFactory.I.CreateView(hero);
            }

            foreach (var enemy in entity.EnemiesByWaves[0].Values)
            {
                BattleUnitViewFactory.I.CreateView(enemy);
            }
        }
    }
}