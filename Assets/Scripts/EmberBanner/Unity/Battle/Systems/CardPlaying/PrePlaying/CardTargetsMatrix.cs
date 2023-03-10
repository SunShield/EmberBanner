﻿using System;
using System.Collections.Generic;
using EmberBanner.Core.Enums.Actions;
using EmberBanner.Core.Enums.Battle;
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

        public Dictionary<BattleUnitCrystalView, List<BattleUnitCrystalView>> RedirectorsMatrix = new();
        public Dictionary<BattleUnitCrystalView, BattleUnitCrystalView> DefaultAttackMatrix = new();

        public void AddAttack(BattleUnitCrystalView initiator, BattleUnitCrystalView target, bool defaultAttack = false)
        {
            AddToMatrix(initiator, target, defaultAttack);
            TryRedirectAttack(initiator, target);

            onAttackMatrixChanged?.Invoke();
        }

        private void AddToMatrix(BattleUnitCrystalView initiator, BattleUnitCrystalView target, bool defaultAttack = false)
        {
            AttackMatrix.Add(initiator, target);
            if (defaultAttack) DefaultAttackMatrix.Add(initiator, target);

            if (!DefenseMatrix.ContainsKey(target))
                DefenseMatrix.Add(target, new());

            DefenseMatrix[target].Add(initiator);
        }
        
        private bool CanRedirect(BattleUnitCrystalView initiator, BattleUnitCrystalView target) 
            => target.Card != null // cannot redirect empty die
               && target.Controller == UnitControllerType.Enemy // only enemy attacks can be redirected
               && initiator.Card.Model.MainTarget == CardMainTargetType.Enemy; // only attacks attacking allies can be redirected

        private void TryRedirectAttack(BattleUnitCrystalView initiator, BattleUnitCrystalView newTarget)
        {
            if (!CanRedirect(initiator, newTarget) || !CanRedirectByRoll(initiator, newTarget))
                return;

            var oldTarget = AttackMatrix[newTarget];
            // enemy targets redirection initiator now
            AttackMatrix[newTarget] = initiator;
            
            // Unit targeted by enemy before redirecting is not defending from him now
            DefenseMatrix[oldTarget].Remove(newTarget);
            if (DefenseMatrix[oldTarget].Count == 0) DefenseMatrix.Remove(oldTarget);
            
            // initiator of redirection now defends from enemy
            if (!DefenseMatrix.ContainsKey(initiator)) DefenseMatrix.Add(initiator, new());
            DefenseMatrix[initiator].Add(newTarget);
            
            // tracking redirection
            if (!RedirectorsMatrix.ContainsKey(newTarget)) RedirectorsMatrix.Add(newTarget, new());
            RedirectorsMatrix[newTarget].Add(initiator);
        }

        public void RemoveAttack(BattleUnitCrystalView initiator)
        {
            var target = AttackMatrix[initiator];
            AttackMatrix.Remove(initiator);
            DefenseMatrix[target].Remove(initiator);
            if (DefenseMatrix[target].Count == 0) DefenseMatrix.Remove(target);

            if (RedirectorsMatrix.ContainsKey(target))
            {
                if (RedirectorsMatrix[target].IndexOf(initiator) == RedirectorsMatrix[target].Count - 1)
                {
                    DefenseMatrix[initiator].Remove(target);
                    if (DefenseMatrix[initiator].Count == 0) DefenseMatrix.Remove(initiator);
                }
                
                RedirectorsMatrix[target].Remove(initiator);
                if (RedirectorsMatrix[target].Count == 0) RedirectorsMatrix.Remove(target);
            }

            if (target.Controller == UnitControllerType.Enemy)
            {
                if (RedirectorsMatrix.ContainsKey(target))
                {
                    var lastRedirector = RedirectorsMatrix[target][^1];
                    AttackMatrix[target] = lastRedirector;
                    
                    if (!DefenseMatrix.ContainsKey(lastRedirector)) DefenseMatrix.Add(lastRedirector, new());
                    DefenseMatrix[lastRedirector].Add(target);
                }
                else if (DefaultAttackMatrix.ContainsKey(target))
                {
                    AttackMatrix[target] = DefaultAttackMatrix[target];
                    if (!DefenseMatrix.ContainsKey(DefaultAttackMatrix[target])) DefenseMatrix.Add(DefaultAttackMatrix[target], new());
                    DefenseMatrix[DefaultAttackMatrix[target]].Add(target);
                }
            }
            
            onAttackMatrixChanged?.Invoke();
        }

        private bool CanRedirectByRoll(BattleUnitCrystalView initiator, BattleUnitCrystalView newTarget)
        {
            // aggressions can redirect attacks if their magnitude is higher
            if (newTarget.CurrentRoll >= initiator.CurrentRoll && initiator.Card.MainActionType == ActionType.Aggression) return false;
            // defenses, however, can redirect if magnitudes are tied
            if (newTarget.CurrentRoll >  initiator.CurrentRoll && initiator.Card.MainActionType == ActionType.Defense) return false;
            
            return true;
        }

        public BattleUnitCrystalView GetClashingCrystal(BattleUnitCrystalView initiator)
        {
            if (initiator.Controller == UnitControllerType.Enemy)
            {
                if (RedirectorsMatrix.ContainsKey(initiator)) return RedirectorsMatrix[initiator][^1];
                if (DefaultAttackMatrix.ContainsKey(initiator))
                {
                    var defaultTarget = DefaultAttackMatrix[initiator];
                    if (!AttackMatrix.ContainsKey(defaultTarget)) return null;
                    if (AttackMatrix[defaultTarget] == initiator) return defaultTarget;
                }
            }
            else
            {
                if (!AttackMatrix.ContainsKey(initiator)) return null;
                    
                var target = AttackMatrix[initiator];
                if (GetClashingCrystal(target) == initiator) return target;
            }

            return null;
        }

        public bool HasAttack(BattleUnitCrystalView initiator) => AttackMatrix.ContainsKey(initiator);

        public event Action onAttackMatrixChanged;
    }
}