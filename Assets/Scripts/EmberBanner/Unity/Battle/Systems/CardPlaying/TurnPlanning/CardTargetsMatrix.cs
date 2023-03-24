using System;
using System.Collections.Generic;
using EmberBanner.Core.Enums.Battle;
using EmberBanner.Core.Service.Classes.Fundamental;
using EmberBanner.Core.Service.Extensions.Targeting;
using EmberBanner.Unity.Battle.Views.Impl.Units.Crystals;

namespace EmberBanner.Unity.Battle.Systems.CardPlaying.TurnPlanning
{
    public class CardTargetsMatrix
    {
        private static CardTargetsMatrix _instance;
        public static CardTargetsMatrix I => _instance ??= new();

        public Dictionary<BattleUnitCrystalView, BattleUnitCrystalView> AttackMatrix = new();
        public NonEmptyListDictionary<BattleUnitCrystalView, BattleUnitCrystalView> DefenseMatrix = new();
        public NonEmptyListDictionary<BattleUnitCrystalView, BattleUnitCrystalView> RedirectorsMatrix = new();
        public Dictionary<BattleUnitCrystalView, BattleUnitCrystalView> DefaultAttackMatrix = new();

        public void AddAttack(BattleUnitCrystalView initiator, BattleUnitCrystalView target, bool defaultAttack = false)
        {
            if (defaultAttack) RegisterDefaultAttack(initiator, target);
            
            AddAttackToMatrix(initiator, target);
            TryRedirectAttack(initiator, target);

            onAttackMatrixChanged?.Invoke();
        }

        private void RegisterDefaultAttack(BattleUnitCrystalView initiator, BattleUnitCrystalView target) => DefaultAttackMatrix.Add(initiator, target);

        private void AddAttackToMatrix(BattleUnitCrystalView initiator, BattleUnitCrystalView target)
        {
            AttackMatrix.Add(initiator, target);
            DefenseMatrix.Add(target, initiator);
        }

        private void TryRedirectAttack(BattleUnitCrystalView initiator, BattleUnitCrystalView newTarget)
        {
            if (!CanRedirect(initiator, newTarget) || !CanRedirectByRoll(initiator, newTarget))
                return;

            var crystalAttackRedirectedFrom = GetTarget(newTarget);
            
            // Unit targeted by enemy before redirecting is not defending from him now
            RemoveAttackerFromDefenseMatrix(crystalAttackRedirectedFrom, newTarget);
            ChangeAttackTarget(newTarget, initiator);
            
            // tracking redirection
            RegisterRedirection(newTarget, initiator);
        }

        public BattleUnitCrystalView GetTarget(BattleUnitCrystalView newTarget) => AttackMatrix.ContainsKey(newTarget) ? AttackMatrix[newTarget] : null;
        private void RemoveAttackerFromDefenseMatrix(BattleUnitCrystalView target, BattleUnitCrystalView initiator) => DefenseMatrix.Remove(target, initiator);
        
        private void ChangeAttackTarget(BattleUnitCrystalView initiator, BattleUnitCrystalView target)
        {
            AttackMatrix[initiator] = target;
            DefenseMatrix.Add(target, initiator);
        }
        
        private void RegisterRedirection(BattleUnitCrystalView redirectedCrystal, BattleUnitCrystalView redirectorCrystal) 
            => RedirectorsMatrix.Add(redirectedCrystal, redirectorCrystal);
        
        private bool CanRedirect(BattleUnitCrystalView initiator, BattleUnitCrystalView target)
        {
            return target.Card != null // cannot redirect empty die
                   && target.Controller == UnitControllerType.Enemy // only enemy attacks can be redirected
                   && initiator.Card.Model.TargetType.AllowsEnemy() // only attacks able to attack you or allies can be redirected
                   && target.Card.CanTarget(initiator);
        }

        private bool CanRedirectByRoll(BattleUnitCrystalView initiator, BattleUnitCrystalView newTarget)
        {
            if (newTarget.CurrentRoll >= initiator.CurrentRoll ) return false;
            
            return true;
        }

        public void RemoveAttack(BattleUnitCrystalView initiator)
        {
            var target = GetTarget(initiator);
            RemoveAttackFromMatrix(initiator);
            RemoveDefenseFromMatrix(initiator, target);
            if (IsCrystalRedirected(target))
            {
                if (IsCrystalCurrentTargetsRedirector(target, initiator))
                    RemoveAttackerFromDefenseMatrix(initiator, target);
                
                DeregisterRedirection(target, initiator);
            }

            if (target.IsEnemy)
            {
                if (IsCrystalRedirected(target))
                {
                    var lastRedirector = GetCrystalLastRedirector(target);
                    ChangeAttackTarget(target, lastRedirector);
                }
                else if (CheckCrystalHasDefaultAttack(target))
                {
                    ChangeAttackTarget(target, GetCrystalDefaultAttack(target));
                }
            }
            
            onAttackMatrixChanged?.Invoke();
        }

        private void RemoveAttackFromMatrix(BattleUnitCrystalView initiator) => AttackMatrix.Remove(initiator);
        private void RemoveDefenseFromMatrix(BattleUnitCrystalView initiator, BattleUnitCrystalView target) => DefenseMatrix.Remove(target, initiator);
        private bool IsCrystalRedirected(BattleUnitCrystalView target) => RedirectorsMatrix.ContainsKey(target);
        private bool IsCrystalCurrentTargetsRedirector(BattleUnitCrystalView target, BattleUnitCrystalView crystal) => RedirectorsMatrix[target].IndexOf(crystal) == RedirectorsMatrix[target].Count - 1;
        private void DeregisterRedirection(BattleUnitCrystalView redirectedCrystal, BattleUnitCrystalView redirectorCrystal) => RedirectorsMatrix.Remove(redirectedCrystal, redirectorCrystal);
        private bool CheckCrystalHasDefaultAttack(BattleUnitCrystalView target) => DefaultAttackMatrix.ContainsKey(target);
        private BattleUnitCrystalView GetCrystalLastRedirector(BattleUnitCrystalView crystal) => RedirectorsMatrix[crystal][^1];
        private BattleUnitCrystalView GetCrystalDefaultAttack(BattleUnitCrystalView target) => DefaultAttackMatrix[target];
        
        public BattleUnitCrystalView GetClashingCrystal(BattleUnitCrystalView initiator)
        {
            if (initiator.IsEnemy)
            {
                if (IsCrystalRedirected(initiator)) return GetCrystalLastRedirector(initiator);
                if (CheckCrystalHasDefaultAttack(initiator))
                {
                    var defaultTarget = GetCrystalDefaultAttack(initiator);
                    if (CheckCrystalAttacksTarget(defaultTarget, initiator)) return defaultTarget;
                }
            }
            else
            {
                if (!CrystalHasAttack(initiator)) return null;
                    
                var target = GetTarget(initiator);
                if (target == initiator) return null; // self-targeting actions
                if (GetClashingCrystal(target) == initiator) return target;
            }

            return null;
        }

        public bool CrystalHasAttack(BattleUnitCrystalView initiator) => AttackMatrix.ContainsKey(initiator);

        private bool CheckCrystalAttacksTarget(BattleUnitCrystalView crystal, BattleUnitCrystalView target)
        {
            if (!CrystalHasAttack(crystal)) return false;
            return AttackMatrix[crystal] == target;
        }

        public bool CheckTargetsSelf(BattleUnitCrystalView initiator) => GetTarget(initiator) == initiator;

        public bool CheckClash(BattleUnitCrystalView initiator, BattleUnitCrystalView target)
            => !initiator.IsDead && !target.IsDead && initiator != target && GetClashingCrystal(initiator) == target;

        public void Clear()
        {
            AttackMatrix.Clear();
            DefenseMatrix.Clear();
            RedirectorsMatrix.Clear();
            DefaultAttackMatrix.Clear();
        }

        public event Action onAttackMatrixChanged;
    }
}