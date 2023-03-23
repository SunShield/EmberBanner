using EmberBanner.Unity.Battle.Views.Impl.Units.Crystals;

namespace EmberBanner.Unity.Battle.Views.Impl.Units
{
    public partial class BattleUnitView
    {
        public static class BattleUnitStateHandler
        {
            private const int PostStaggerWillRegenMultiplier = 5;
            
            #region Turn Start

            public static void OnTurnStart(BattleUnitView unit, bool isFirstTurn)
            {
                RegenerateStats(unit, isFirstTurn);
                DrawCards(unit, isFirstTurn);
                RemoveTempStats(unit);
            }

            private static void RegenerateStats(BattleUnitView unit, bool isFirstTurn)
            {
                if (isFirstTurn) return;
                
                RegenerateUnitHealth(unit);
                RegenerateUnitWill(unit);
                RegenerateUnitEnergy(unit);
            }

            private static void RegenerateUnitHealth(BattleUnitView unit) => unit.Entity.ChangeHealth(unit.Entity.HealthRegen.CalculateValue());
            
            private static void RegenerateUnitWill(BattleUnitView unit)
            {
                // When unit leaves stagger, it regenerated a lot more will
                var regenerationAmount = unit.Entity.WillRegen.CalculateValue();
                if (unit.IsStaggered)
                    regenerationAmount *= PostStaggerWillRegenMultiplier;
                unit.Entity.ChangeWill(regenerationAmount);
            }

            private static void RegenerateUnitEnergy(BattleUnitView unit) => unit.Entity.ChangeEnergy(unit.Entity.EnergyRegen.CalculateValue());

            private static void RemoveTempStats(BattleUnitView unit)
            {
                unit.Entity.ChangeShield(-unit.Entity.CurrentShield);
                unit.Entity.ChangeField(-unit.Entity.CurrentField);
            }
            
            private static void DrawCards(BattleUnitView unit, bool isFirstTurn)
            {
                if (isFirstTurn) unit._zonesManager.DrawCardsAtBattleStart();
                else             unit._zonesManager.DrawCardsAtTurnStart();
            }

            #endregion

            #region Turn Pre Plan

            public static void OnTurnPrePlan(BattleUnitView unit)
            {
                OnCrystalsPrePlan(unit);
            }

            private static void OnCrystalsPrePlan(BattleUnitView unit)
            {
                foreach (var crystal in unit._unitCrystals.Crystals)
                {
                    OnCrystalPrePlan(unit, crystal);
                }
            }

            private static void OnCrystalPrePlan(BattleUnitView unit, BattleUnitCrystalView crystal)
            {
                BattleUnitCrystalView.BattleCrystalStateHandler.OnTurnPrePlan(crystal);
            }

            #endregion

            #region Turn End

            public static void OnTurnEnd(BattleUnitView unit)
            {
                OnCrystalsTurnEnd(unit);
            }

            private static void OnCrystalsTurnEnd(BattleUnitView unit)
            {
                foreach (var crystal in unit._unitCrystals.Crystals)
                {
                    OnCrystalTurnEnd(unit, crystal);
                }
            }
            
            private static void OnCrystalTurnEnd(BattleUnitView unit, BattleUnitCrystalView crystal)
            {
                BattleUnitCrystalView.BattleCrystalStateHandler.OnTurnEnd(crystal);
                if (crystal.Card == null) return;

                var card = crystal.Card;
                unit._zonesManager.UnsetCardPrePlayed(crystal.Card, crystal);
                unit._zonesManager.MoveToGraveyard(card);
            }

            #endregion
        }
    }
}