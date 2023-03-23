namespace EmberBanner.Unity.Battle.Views.Impl.Units.Crystals
{
    public partial class BattleUnitCrystalView
    {
        public static class BattleCrystalStateHandler
        {
            public static void OnTurnPrePlan(BattleUnitCrystalView crystal)
            {
                crystal.Roll();
            }
            
            public static void OnTurnEnd(BattleUnitCrystalView crystal)
            {
                crystal.OnTurnEnd();
            }
        }
    }
}