using System.Collections.Generic;
using System.Linq;
using EmberBanner.Core.Enums.Battle;
using EmberBanner.Unity.Battle.Management;
using EmberBanner.Unity.Battle.Views.Impl.Units.Crystals;

namespace EmberBanner.Unity.Battle.Systems.TurnOrder
{
    public class TurnOrderController
    {
        private static TurnOrderController _instance;
        public static TurnOrderController I => _instance ??= new();
        
        private const int NoCrystalTurn = -1;
        
        public List<BattleUnitCrystalView> Crystals { get; private set; } = new();
        public int CurrentCrystalIndex { get; private set; } = NoCrystalTurn;
        public bool AllCrystalsEndedTurns => CurrentCrystalIndex == Crystals.Count;
        public BattleUnitCrystalView CurrentCrystal => Crystals[CurrentCrystalIndex];

        public void DetermineTurnOrder()
        {
            CurrentCrystalIndex = NoCrystalTurn;
            Crystals.Clear();

            var units = BattleManager.I.Registry.Units.Values.ToList();
            var crystals = units.SelectMany(u => u.UnitCrystals.Crystals).ToList();
            crystals.Sort((crystal1, crystal2) =>
            {
                if (crystal1.CurrentRoll > crystal2.CurrentRoll) return 1;
                if (crystal2.CurrentRoll > crystal1.CurrentRoll) return -1;

                var isRoll1Player = crystal1.Controller == UnitControllerType.Player;
                var isRoll2Player = crystal2.Controller == UnitControllerType.Player;

                if (isRoll1Player && !isRoll2Player) return 1;
                if (!isRoll1Player && isRoll2Player) return -1;

                return 0;
                
                // TODO: implement spot order logic later
            });

            Crystals = crystals;
        }

        public void AdvanceOrder()
        {
            CurrentCrystalIndex++;
        }
    }
}