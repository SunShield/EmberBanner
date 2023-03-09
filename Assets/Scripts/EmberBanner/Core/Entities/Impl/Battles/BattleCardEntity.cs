using EmberBanner.Core.Entities.Impl.Cards;
using EmberBanner.Core.Entities.Impl.Units;
using EmberBanner.Core.Enums.Battle;
using EmberBanner.Core.Models.Cards;

namespace EmberBanner.Core.Ingame.Impl.Battles
{
    public class BattleCardEntity : CardEntity
    {
        public BattleCardZone Zone { get; private set; } = BattleCardZone.None;
        
        public BattleCardEntity(int id, CardModel model) : base(id, model)
        {
        }

        public void SetZone(BattleCardZone zone)
        {
            // on-zone-enter-effects-here
            Zone = zone;
        }

        public override void Initialize(object payload)
        {
            SetOwner((UnitEntity)payload);
        }
    }
}