using System.Collections.Generic;
using System.Linq;
using EmberBanner.Core.Entities.Impl.Units;
using EmberBanner.Core.Entities.Management.Factories.Impl.Units;
using EmberBanner.Core.Models.Battles;

namespace EmberBanner.Core.Ingame.Impl.Battles
{
    public class BattleEntity : AbstractEntity<BattleModel>
    {
        public List<Dictionary<int, UnitEntity>> EnemiesByWaves { get; private set; } = new();
        
        public BattleEntity(int id, BattleModel model) : base(id, model)
        {
            var maxWaves = model.DeterminedEnemies.Max(e => e.Wave);
            for(int i = 0; i < maxWaves; i++)
                EnemiesByWaves.Add(new());
            
            foreach (var determinedEnemy in model.DeterminedEnemies)
            {
                var enemy = UnitEntityFactory.I.CreateEntity(determinedEnemy.UnitName, true);
                EnemiesByWaves[determinedEnemy.Wave - 1].Add(enemy.Id, enemy);
            }
            
            // Enemies Generation here later (generation params can be added in model)
        }
    }
}