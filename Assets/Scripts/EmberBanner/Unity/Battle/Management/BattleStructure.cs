using System.Collections.Generic;
using EmberBanner.Unity.Battle.Systems.UnitSpotSystem;
using EmberBanner.Unity.Service;
using UnityEngine;

namespace EmberBanner.Unity.Battle.Management
{
    public class BattleStructure : EBMonoBehaviour
    {
        [SerializeField] private List<UnitSpot> _playerSpots;
        [SerializeField] private List<UnitSpot> _enemySpots;

        public List<UnitSpot> PlayerSpots => _playerSpots;
        public List<UnitSpot> EnemySpots => _enemySpots;

        public UnitSpot GetFreePlayerSpot()
        {
            foreach (var playerSpot in _playerSpots)
            {
                if (!playerSpot.IsFree) continue;
                return playerSpot;
            }

            return null;
        }
        
        public UnitSpot GetFreeEnemySpot()
        {
            foreach (var enemySpot in _enemySpots)
            {
                if (!enemySpot.IsFree) continue;
                return enemySpot;
            }

            return null;
        }
    }
}