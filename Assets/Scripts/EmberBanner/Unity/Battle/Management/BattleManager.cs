using EmberBanner.Unity.Service;
using UnityEngine;

namespace EmberBanner.Unity.Battle.Management
{
    public class BattleManager : EBMonoBehaviour
    {
        private static BattleManager _instance;
        public static BattleManager I
        {
            get
            {
                if (_instance == null)
                    _instance = FindObjectOfType<BattleManager>();
                return _instance;
            }
        }

        [SerializeField] private BattleStructure _structure;
        
        public BattleRegistry Registry { get; private set; } = new();
        public BattleStructure Structure => _structure;
    }
}