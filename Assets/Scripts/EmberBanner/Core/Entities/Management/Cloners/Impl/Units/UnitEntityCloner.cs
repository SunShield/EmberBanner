using EmberBanner.Core.Entities.Impl.Units;
using EmberBanner.Core.Entities.Management.SaveLoad.Data.Impl.Units;
using EmberBanner.Core.Ingame.Management.Cloners.Impl.Cards;
using EmberBanner.Core.Models.Units;

namespace EmberBanner.Core.Ingame.Management.Cloners.Impl.Units
{
    public class UnitEntityCloner : SavableEntityCloner<UnitEntity, UnitModel, UnitSaveData>
    {
        private static UnitEntityCloner _instance;
        public static UnitEntityCloner I => _instance ??= new();

        protected override void ProcessEntityPostInitialize(UnitEntity originalEntity, UnitEntity entityClone)
        {
            /*// cloning crystals
            foreach (var originalEntityCrystal in originalEntity.Crystals)
            {
                var crystalClone = UnitCrystalEntityCloner.I.Clone(originalEntityCrystal);
                entityClone.Crystals.Add(crystalClone);
            }

            // cloning deck cards
            foreach (var originalEntityCardKey in originalEntity.Deck.Keys)
            {
                var originalEntityCard = originalEntity.Deck[originalEntityCardKey];
                var entityCardClone = CardEntityCloner.I.Clone(originalEntityCard);
                entityClone.AddCard(entityCardClone);
            }*/
        }
    }
}