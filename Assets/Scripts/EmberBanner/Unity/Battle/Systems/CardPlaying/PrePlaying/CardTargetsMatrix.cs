using System;
using System.Collections.Generic;
using EmberBanner.Core.Enums.Actions;
using EmberBanner.Unity.Battle.Views.Impl.Units.Crystals;

namespace EmberBanner.Unity.Battle.Systems.CardPlaying.PrePlaying
{
    public class CardTargetsMatrix
    {
        private static CardTargetsMatrix _instance;
        public static CardTargetsMatrix I => _instance ??= new();

        /// <summary>
        /// Key is a crystal holding attack, Value is a list of defenders. Non-AoE attacks always have ONE defender
        /// </summary>
        public Dictionary<BattleUnitCrystalView, BattleUnitCrystalView> AttackMatrix = new();
        
        /// <summary>
        /// Key is a crystal targeted by an attack, Value is a list of crystals targeting this crystal
        /// </summary>
        public Dictionary<BattleUnitCrystalView, List<BattleUnitCrystalView>> DefenseMatrix = new();

        public void AddAttack(BattleUnitCrystalView initiator, BattleUnitCrystalView target)
        {
            AddToMatrix(initiator, target);

            if (CanRedirect(initiator, target))
            {
                TryRedirectAttack(initiator, target);
            }

            onAttackMatrixChanged?.Invoke();
        }

        private void AddToMatrix(BattleUnitCrystalView initiator, BattleUnitCrystalView target)
        {
            AttackMatrix.Add(initiator, target);

            if (!DefenseMatrix.ContainsKey(target))
                DefenseMatrix.Add(target, new());

            DefenseMatrix[target].Add(initiator);
        }
        
        private bool CanRedirect(BattleUnitCrystalView initiator, BattleUnitCrystalView target) 
            => target.Card != null && target.Controller != initiator.Controller && initiator.Card.Entity.MainAction.Model.Type != ActionType.Support;

        private void TryRedirectAttack(BattleUnitCrystalView initiator, BattleUnitCrystalView newTarget)
        {
            // aggressions can redirect attacks if their magnitude is higher
            if (newTarget.CurrentRoll >= initiator.CurrentRoll && initiator.Card.MainActionType == ActionType.Aggression) return;
            // defenses, however, can redirect if magnitudes are tied
            if (newTarget.CurrentRoll >  initiator.CurrentRoll && initiator.Card.MainActionType == ActionType.Defense) return;

            var oldTarget = AttackMatrix[initiator];
            AttackMatrix[newTarget] = initiator;
            
            DefenseMatrix[oldTarget].Remove(initiator);
            if (DefenseMatrix[oldTarget].Count == 0) DefenseMatrix.Remove(oldTarget);
            
            if (!DefenseMatrix.ContainsKey(initiator)) DefenseMatrix.Add(initiator, new());
            DefenseMatrix[initiator].Add(newTarget);
        }

        public void RemoveAttack(BattleUnitCrystalView initiator)
        {
            var isAoe = initiator.Card.Entity.MainAction.Model.IsAoE;
            AttackMatrix.Remove(initiator);
            
            onAttackMatrixChanged?.Invoke();
        }

        public BattleUnitCrystalView GetClashingCrystal(BattleUnitCrystalView initiator)
        {
            var defender = AttackMatrix[initiator];
            if (DefenseMatrix[defender][0] != initiator) return null;
            if (!AttackMatrix.ContainsKey(defender)) return null;
            if (AttackMatrix[defender] != initiator) return null;

            return AttackMatrix[initiator];
        }

        public bool HasAttack(BattleUnitCrystalView initiator) => AttackMatrix.ContainsKey(initiator);

        public event Action onAttackMatrixChanged;
    }
}