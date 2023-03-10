using System;
using System.Collections.Generic;
using EmberBanner.Unity.Battle.Views.Impl.Units.Crystals;

namespace EmberBanner.Unity.Battle.Systems.CardPlaying.PrePlaying
{
    public class CardTargetsMatrix
    {
        private static CardTargetsMatrix _instance;
        public static CardTargetsMatrix I => _instance ??= new();

        public Dictionary<BattleUnitCrystalView, List<BattleUnitCrystalView>> AttackMatrix = new();

        public void AddAttack(BattleUnitCrystalView initiator, BattleUnitCrystalView target)
        {
            if (!AttackMatrix.ContainsKey(initiator))
                AttackMatrix.Add(initiator, new());
            
            AttackMatrix[initiator].Add(target);
            onAttackAdded?.Invoke(initiator, target);
        }

        public void RemoveAttack(BattleUnitCrystalView initiator)
        {
            AttackMatrix.Remove(initiator);
            onAttackRemoved?.Invoke(initiator);
        }

        public bool HasAttack(BattleUnitCrystalView initiator) => AttackMatrix.ContainsKey(initiator);

        public event Action<BattleUnitCrystalView, BattleUnitCrystalView> onAttackAdded;
        public event Action<BattleUnitCrystalView> onAttackRemoved;
    }
}