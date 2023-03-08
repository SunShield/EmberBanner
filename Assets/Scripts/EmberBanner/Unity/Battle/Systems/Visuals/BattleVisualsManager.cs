using EmberBanner.Core.Ingame.Impl.Battles;
using EmberBanner.Core.Service.Debug;
using EmberBanner.Unity.Service;
using UnityEngine;

namespace EmberBanner.Unity.Battle.Systems.Visuals
{
    public class BattleVisualsManager : EBMonoBehaviour
    {
        private static BattleVisualsManager _instance;
        public static BattleVisualsManager I
        {
            get
            {
                if (_instance == null)
                    _instance = FindObjectOfType<BattleVisualsManager>();
                return _instance;
            }
        }
        
        [SerializeField] private SpriteRenderer _battleBackground;

        public void SetVisuals(BattleEntity battle)
        {
            EBDebugger.Log(EBDebugContext.Battle, "Setting Battle Visuals");
            _battleBackground.sprite = battle.Model.Sprite;
        }
    }
}