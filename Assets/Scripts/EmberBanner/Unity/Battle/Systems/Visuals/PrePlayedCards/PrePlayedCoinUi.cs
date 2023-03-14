using EmberBanner.Unity.Service;
using UnityEngine;

namespace EmberBanner.Unity.Battle.Systems.Visuals.PrePlayedCards
{
    public class PrePlayedCoinUi : EBMonoBehaviour
    {
        private readonly Color _defaultColor = new (1f, 0.58f, 0f, 1f);
        
        [SerializeField] private SpriteRenderer _graphics;
        
        public void SetState(bool? state)
        {
            if (state == null)
            {
                _graphics.color = _defaultColor;
                return;
            }
            
            if      (state.Value)  _graphics.color = Color.green;
            else if (!state.Value) _graphics.color = Color.red;
        }
    }
}