using System.Collections.Generic;
using EmberBanner.Core.Entities.Impl.Cards;
using EmberBanner.Core.Entities.Impl.Units.Crystals;
using EmberBanner.Core.Entities.Management.Factories.Impl.Units;
using EmberBanner.Core.Entities.Management.SaveLoad.Data.Impl.Units;
using EmberBanner.Core.Ingame.Management.SaveLoad;
using EmberBanner.Core.Models.Units;
using Plugins.ComplexValue;

namespace EmberBanner.Core.Entities.Impl.Units
{
    public class UnitEntity : SavableEntity<UnitModel, UnitSaveData>
    {
        public ComplexValue StartingHealth { get; private set; }
        public ComplexValue MaxHealth { get; private set; }
        public ComplexValue StartingEnergy { get; private set; }
        public ComplexValue MaxEnergy { get; private set; }
        public ComplexValue HandSize { get; private set; }
        public ComplexValue Draw { get; private set; }
        public List<UnitCrystalEntity> Crystals { get; private set; } = new();
        
        private Dictionary<int, CardEntity> _deck = new();
        public IReadOnlyDictionary<int, CardEntity> Deck => _deck;

        public UnitEntity(int id, UnitModel model) : base(id, model)
        {
            StartingHealth = new(true, model.StartingHealth);
            MaxHealth      = new(true, model.MaxHealth);
            StartingEnergy = new(true, model.StartingEnergy);
            MaxEnergy      = new(true, model.MaxEnergy);
            HandSize       = new(true, model.HandSize);
            Draw           = new(true, model.Draw);
            
            foreach (var crystalModel in model.Crystals)
            {
                var crystalEntity = UnitCrystalEntityFactory.I.CreateEntity(crystalModel, IsTemporary);
                Crystals.Add(crystalEntity);
            }
        }

        public override UnitSaveData GenerateSaveDataInternal(UnitSaveData saveData)
        {
            return saveData;
        }

        public void AddCard(CardEntity card)
        {
            _deck.Add(card.Id, card);
            // On-Added-To-Deck card effects here
            // On-another-card-added-to-deck-effects here
        }

        public void RemoveCard(CardEntity card)
        {
            // On-Removed-From-Deck card effects here
            // On-another-card-removed-from-deck-effects here
            _deck.Remove(card.Id);
        }
    }
}