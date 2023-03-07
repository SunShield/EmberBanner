using System.Collections.Generic;
using EmberBanner.Core.Entities.Impl.Cards;
using EmberBanner.Core.Entities.Impl.Units.Crystals;
using EmberBanner.Core.Entities.Management.Factories.Impl.Units;
using EmberBanner.Core.Entities.Management.SaveLoad.Data.Impl.Units;
using EmberBanner.Core.Ingame.Management.SaveLoad;
using EmberBanner.Core.Models.Units;

namespace EmberBanner.Core.Entities.Impl.Units
{
    public class UnitEntity : SavableEntity<UnitModel, UnitSaveData>
    {
        public List<UnitCrystalEntity> Crystals { get; private set; } = new();
        public List<CardEntity> Deck { get; private set; } = new();

        public UnitEntity(int id, UnitModel model) : base(id, model)
        {
            foreach (var crystalModel in model.Crystals)
            {
                var crystalEntity = UnitCrystalEntityFactory.I.CreateEntity(crystalModel);
                Crystals.Add(crystalEntity);
            }
        }

        public override UnitSaveData GenerateSaveDataInternal(UnitSaveData saveData)
        {
            return saveData;
        }
    }
}