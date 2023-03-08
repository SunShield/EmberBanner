using System.Collections.Generic;
using EmberBanner.Core.Models.Battles;

namespace EmberBanner.Core.Ingame.Impl.Battles
{
    public class BattleEntity : AbstractEntity<BattleModel>
    {
        // in future, possibly, we will have "reserved" heroes who approach onto the battlefield if others die
        public List<BattleUnitEntity> Heroes { get; private set; } = new();
        public List<Dictionary<int, BattleUnitEntity>> EnemiesByWaves { get; private set; } = new();
        
        public BattleEntity(int id, BattleModel model) : base(id, model)
        {
        }
    }
}