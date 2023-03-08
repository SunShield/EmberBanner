using System;

namespace EmberBanner.Core.Models.Units.Cards
{
    /// <summary>
    /// Possibly will hold some data for actual card upgrades etc
    /// </summary>
    [Serializable]
    public class UnitDefaultCardModel : AbstractModel
    {
        public string CardName;
        
        /// <summary>
        /// How many copies of this card are added to unit's deck
        /// </summary>
        public int Amount;
    }
}