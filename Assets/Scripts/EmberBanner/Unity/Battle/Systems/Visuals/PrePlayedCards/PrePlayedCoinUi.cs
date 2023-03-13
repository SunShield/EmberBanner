using EmberBanner.Unity.Service;
using UnityEngine;

namespace EmberBanner.Unity.Battle.Systems.Visuals.PrePlayedCards
{
    public class PrePlayedCoinUi : EBMonoBehaviour
    {
        [SerializeField] private SpriteRenderer _graphics;

        public void SetState(bool state)
        {
            if (state) _graphics.color = Color.green;
            else       _graphics.color = Color.red;
        }
    }
}