using EmberBanner.Core.Entities.Impl.Units;
using EmberBanner.Core.Enums.Battle;
using EmberBanner.Core.Models.Units;

namespace EmberBanner.Core.Ingame.Impl.Battles
{
    /// <summary>
    /// In-battle only unit
    ///
    /// Contains some additional battle-related info
    /// </summary>
    public class BattleUnitEntity : UnitEntity
    {
        public UnitControllerType Controller { get; private set; }
        
        public BattleUnitEntity(int id, UnitModel model) : base(id, model)
        {
        }

        public override void Initialize(object payload)
        {
            Controller = (UnitControllerType)payload;
        }
    }
}