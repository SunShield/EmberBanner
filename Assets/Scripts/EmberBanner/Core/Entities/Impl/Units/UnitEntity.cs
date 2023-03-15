using System.Collections.Generic;
using EmberBanner.Core.Entities.Impl.Cards;
using EmberBanner.Core.Entities.Impl.Units.Crystals;
using EmberBanner.Core.Entities.Management.SaveLoad.Data.Impl.Units;
using EmberBanner.Core.Ingame.Management.SaveLoad;
using EmberBanner.Core.Models.Units;
using Plugins.ComplexValue;

namespace EmberBanner.Core.Entities.Impl.Units
{
    public class UnitEntity : SavableEntity<UnitModel, UnitSaveData>
    {
        protected List<UnitCrystalEntity> Crystals { get; } = new();
        protected Dictionary<int, CardEntity> Deck { get; } = new();
        
        public ComplexValue StartingHealth { get; }
        public ComplexValue MaxHealth      { get; }
        public ComplexValue HealthRegen    { get; }
        public ComplexValue StartingWill   { get; }
        public ComplexValue MaxWill        { get; }
        public ComplexValue WillRegen      { get; }
        public ComplexValue StartingEnergy { get; }
        public ComplexValue MaxEnergy      { get; }
        public ComplexValue EnergyRegen    { get; }
        public ComplexValue HandSize       { get; }
        public ComplexValue Draw           { get; }

        public UnitEntity(int id, UnitModel model) : base(id, model)
        {
            StartingHealth = new (true, model.StartingHealth);
            MaxHealth      = new (true, model.MaxHealth);
            HealthRegen    = new (true, model.HealthRegen);
            StartingWill   = new (true, model.StartingWill);
            MaxWill        = new (true, model.MaxWill);
            WillRegen      = new (true, model.WillRegen);
            StartingEnergy = new (true, model.StartingEnergy);
            MaxEnergy      = new (true, model.MaxEnergy);
            EnergyRegen    = new (true, model.EnergyRegen);
            HandSize       = new (true, model.HandSize);
            Draw           = new (true, model.Draw);
        }

        protected override UnitSaveData GenerateSaveDataInternal(UnitSaveData saveData)
        {
            return saveData;
        }

        public void AddCrystal(UnitCrystalEntity crystal) => Crystals.Add(crystal);

        public void AddCard(CardEntity card)
        {
            Deck.Add(card.Id, card);
            // On-Added-To-Deck card effects here
            // On-another-card-added-to-deck-effects here
        }

        public void RemoveCard(CardEntity card)
        {
            // On-Removed-From-Deck card effects here
            // On-another-card-removed-from-deck-effects here
            Deck.Remove(card.Id);
        }

        /*/// <summary>
        /// Copying entity is needed to apply stuff to the copy without
        /// concerning on breaking original
        /// The main purpose of entity copies is using them in battle:
        /// Hero copies in battle can be freely changed and original is not broken
        /// </summary>
        /// <returns></returns>
        public UnitEntity CreateCopy()
        {
            var entity = new UnitEntity(Id, Model);
            entity.PostLoad(GenerateSaveData());
            return entity;
        }*/
    }
}