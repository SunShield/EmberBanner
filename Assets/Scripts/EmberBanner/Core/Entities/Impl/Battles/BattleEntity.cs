using System.Collections.Generic;
using EmberBanner.Core.Entities.Impl.Units;
using EmberBanner.Core.Models.Battles;

namespace EmberBanner.Core.Ingame.Impl.Battles
{
    public class BattleEntity : AbstractEntity<BattleModel>
    {
        public List<UnitEntity> Heroes { get; private set; } = new();
        public List<Dictionary<int, UnitEntity>> EnemiesByWaves { get; private set; } = new();
        
        public BattleEntity(int id, BattleModel model) : base(id, model)
        {
        }
    }
}