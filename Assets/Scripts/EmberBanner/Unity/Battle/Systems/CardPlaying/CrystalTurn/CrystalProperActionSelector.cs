using EmberBanner.Core.Ingame.Impl.Battles;
using EmberBanner.Unity.Battle.Views.Impl.Units.Crystals;

namespace EmberBanner.Unity.Battle.Systems.CardPlaying.CrystalTurn
{
    /// <summary>
    /// Logic behind choosing an action to be played in certain moment is not simple
    /// </summary>
    public class CrystalProperActionSelector
    {
        private static CrystalProperActionSelector _instance;
        public static CrystalProperActionSelector I => _instance ??= new();

        public BattlePlayingActionEntity SelectProperAction(BattleUnitCrystalView initiator, BattleUnitCrystalView target)
        {
            
            
            return null;
        }
    }
}