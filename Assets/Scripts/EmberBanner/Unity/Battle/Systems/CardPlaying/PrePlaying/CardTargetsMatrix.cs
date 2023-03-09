using System.Collections.Generic;
using EmberBanner.Core.Ingame.Impl.Battles;

namespace EmberBanner.Unity.Battle.Systems.CardPlaying.PrePlaying
{
    public class CardTargetsMatrix
    {
        private static CardTargetsMatrix _instance;
        public static CardTargetsMatrix I => _instance ??= new();

        private Dictionary<int, int> EnemyDefaultTargetMatrix { get; } = new();

        public void AddDefaultTarget(BattleUnitCrystalEntity initiator, BattleUnitCrystalEntity target)
        {
            EnemyDefaultTargetMatrix.Add(initiator.Id, target.Id);
        }
    }
}