using System.Linq;
using EmberBanner.Core.Enums.Battle;
using EmberBanner.Core.Ingame.Impl.Battles;
using EmberBanner.Core.Ingame.Management.Factories;
using EmberBanner.Core.Models.Battles;
using EmberBanner.Core.Service.Debug;
using EmberBanner.Unity.Data;

namespace EmberBanner.Core.Entities.Management.Factories.Impl.Battles
{
    public class BattleEntityFactory : EntityFactory<BattleEntity, BattleModel>
    {
        private static BattleEntityFactory _instance;
        public static BattleEntityFactory I => _instance ??= new();

        public BattleEntity CreateEntity(string modelKey)
        {
            var model = DataHolder.I.Databases.Battles[modelKey];
            return CreateEntity(model, true);
        }

        protected override void OnPostCreateEntity(BattleEntity entity, BattleModel model)
        {
            var maxWaves = model.DeterminedEnemies.Max(e => e.Wave);
            for(int i = 0; i < maxWaves; i++)
                entity.EnemiesByWaves.Add(new());
            
            foreach (var determinedEnemy in model.DeterminedEnemies)
            {
                var enemy = BattleUnitEntityFactory.I.CreateEntity(determinedEnemy.UnitName, UnitControllerType.Enemy);
                entity.EnemiesByWaves[determinedEnemy.Wave - 1].Add(enemy.Id, enemy);
            }
            
            // Enemies Generation here later (generation params can be added in model)
            
            EBDebugger.Log(EBDebugContext.Entities, EBDebugContext.Battle, $"Battle Entity (id: {entity.Id} | model: {model.Name}) created");
        }
    }
}