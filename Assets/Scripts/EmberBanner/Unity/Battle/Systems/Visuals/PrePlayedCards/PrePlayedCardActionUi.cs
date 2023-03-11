using System.Collections.Generic;
using EmberBanner.Core.Entities.Impl.Cards;
using EmberBanner.Core.Enums.Actions;
using EmberBanner.Unity.Service;
using UnityEngine;

namespace EmberBanner.Unity.Battle.Systems.Visuals.PrePlayedCards
{
    public class PrePlayedCardActionUi : EBMonoBehaviour
    {
        private readonly Dictionary<ActionType, Color> _actionColors = new()
        {
            { ActionType.Aggression, new(0.9f, 0.2f, 0.2f, 1f) },
            { ActionType.Defense,    new(0.1f, 0.5f, 0.9f, 1f) },
            { ActionType.Support,    new(0.4f, 0.8f, 0.4f, 1f) },
        };

        [SerializeField] private SpriteRenderer _graphics;
        [SerializeField] private PrePlayedCoinUi _coinPrefab;
        [SerializeField] private Transform _coinsOrigin;

        private List<PrePlayedCoinUi> _coins = new();

        public void Initialize(CardActionEntity action)
        {
            _graphics.color = _actionColors[action.Model.Type];

            var coinPosition = _coinsOrigin.position;
            for (int i = 0; i < action.Model.CoinsAmount; i++)
            {
                var coin = Instantiate(_coinPrefab, coinPosition, Quaternion.identity, _coinsOrigin);
                _coins.Add(coin);
            }
        }
    }
}