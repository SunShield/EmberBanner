using System.Linq;
using EmberBanner.Core.Entities.Management.Factories.Impl.Units;
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
            var model = DataHolder.I.Data.Battles[modelKey];
            return CreateEntity(model, true);
        }

        protected override void OnPostCreateEntity(BattleEntity entity, BattleModel model)
        {
            var maxWaves = model.DeterminedEnemies.Max(e => e.Wave);
            for(int i = 0; i < maxWaves; i++)
                entity.EnemiesByWaves.Add(new());
            
            foreach (var determinedEnemy in model.DeterminedEnemies)
            {
                var enemy = UnitEntityFactory.I.CreateEntity(determinedEnemy.UnitName, true);
                entity.EnemiesByWaves[determinedEnemy.Wave - 1].Add(enemy.Id, enemy);
            }
            
            // Enemies Generation here later (generation params can be added in model)
            
            EBDebugger.Log(EBDebugContext.Entities, EBDebugContext.Battle, $"Battle Entity (id: {entity.Id} | model: {model.Name}) created");
        }
    }
}