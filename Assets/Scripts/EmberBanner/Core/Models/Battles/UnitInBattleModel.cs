using System;

namespace EmberBanner.Core.Models.Battles
{
    /// <summary>
    /// Holds battle-specific info of unit: unique buffs, unique stats etc
    /// </summary>
    [Serializable]
    public class UnitInBattleModel : AbstractModel
    {
        public string UnitName;
        public int Wave;
        
        public UnitInBattleModel(string name)
        {
            Name = name;
        }
    }
}