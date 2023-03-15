using EmberBanner.Core.Entities.Management.Factories.Impl.Battles;
using EmberBanner.Core.Enums.Battle;
using EmberBanner.Core.Ingame.Impl.Battles;
using EmberBanner.Core.Service.Debug;
using EmberBanner.Unity.Battle.Management;
using EmberBanner.Unity.Battle.Systems.CardZonesSystem;
using EmberBanner.Unity.Battle.Systems.Visuals;
using EmberBanner.Unity.Battle.Views.Factories.Impl;
using EmberBanner.Unity.Service;

namespace EmberBanner.Unity.Battle.Systems.Startup
{
    public class BattleStarter : EBMonoBehaviour
    {
        private static BattleStarter _instance;
        public static BattleStarter I
        {
            get
            {
                if (_instance == null)
                    _instance = FindObjectOfType<BattleStarter>();
                return _instance;
            }
        }
        
        public void StartBattle()
        {
            var battle = BattleEntityFactory.I.CreateEntity("Test Battle");
            AddTestHeroes(battle);
            AddTestDecks(battle);
            BattleManager.I.InitializeBattle(battle);
            PrepareBattle(battle);
        }

        private void PrepareBattle(BattleEntity battle)
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
            
            var hero2 = BattleUnitEntityFactory.I.CreateEntity("Stalli Hors", UnitControllerType.Player);
            entity.Heroes.Add(hero2);
        }

        private void AddTestDecks(BattleEntity entity)
        {
            foreach (var hero in entity.Heroes)
            {
                foreach (var unitDefaultCardModel in hero.Model.DefaultCards)
                {
                    for (int i = 0; i < unitDefaultCardModel.Amount; i++)
                    {
                        var cardEntity = BattleCardEntityFactory.I.CreateEntity(unitDefaultCardModel.CardName, hero);
                        hero.AddCard(cardEntity);
                    }
                }
            }

            foreach (var enemy in entity.EnemiesByWaves[0].Values)
            {
                foreach (var unitDefaultCardModel in enemy.Model.DefaultCards)
                {
                    for (int i = 0; i < unitDefaultCardModel.Amount; i++)
                    {
                        var cardEntity = BattleCardEntityFactory.I.CreateEntity(unitDefaultCardModel.CardName, enemy);
                        enemy.AddCard(cardEntity);
                    }
                }
            }
        }

        private void BuildViews(BattleEntity entity)
        {
            BuildUnitViews(entity);
            BuildCardViews();
        }

        private void BuildUnitViews(BattleEntity entity)
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

        private void BuildCardViews()
        {
            foreach (var unit in BattleManager.I.Registry.Units.Values)
            {
                UnitCardZonesFactory.I.CreateCardZones(unit);
            }
        }
    }
}