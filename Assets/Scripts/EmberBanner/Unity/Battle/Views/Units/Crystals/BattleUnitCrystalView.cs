using EmberBanner.Core.Entities.Impl.Units.Crystals;
using EmberBanner.Core.Models.Units.Crystals;
using UnityEngine;

namespace EmberBanner.Unity.Battle.Views.Units.Crystals
{
    public class BattleUnitCrystalView : BattleView<UnitCrystalModel, UnitCrystalEntity>
    {
        public BattleUnitView Owner { get; private set; }
        public int? Roll { get; private set; }

        public void Initialize(UnitCrystalEntity entity, BattleUnitView owner)
        {
            Owner = owner;
            Entity = entity;
        }

        public void DoRoll()
        {
            Roll = Random.Range(Model.RollBounds.Min, Model.RollBounds.Max + 1);
        }
    }
}