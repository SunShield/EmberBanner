using System.Collections.Generic;
using EmberBanner.Core.Enums.Actions;
using EmberBanner.Core.Ingame.Impl.Battles;
using EmberBanner.Unity.Battle.Systems.Visuals.PrePlayedCards;
using EmberBanner.Unity.Data;
using EmberBanner.Unity.Service;
using TMPro;
using UnityEngine;

namespace EmberBanner.Unity.Battle.Systems.Visuals.ActionsResolve
{
    public class CurrentActionUi : EBMonoBehaviour
    {
        [SerializeField] private SpriteRenderer _backgroundSprite;
        [SerializeField] private TextMeshPro _magnitudeRoll;
        [SerializeField] private SpriteRenderer _magnitudeTypeSprite;
        [SerializeField] private GameObject _thresholdBlock;
        [SerializeField] private TextMeshPro _thresholdText;
        [SerializeField] private TextMeshPro _descriptionText;
        [SerializeField] private TextMeshPro _currentRollText;
        [SerializeField] private List<PrePlayedCoinUi> _coins;

        public void SetAction(BattlePlayingActionEntity action)
        {
            gameObject.SetActive(true);
            
            _magnitudeRoll.text = action.Magnitude.CalculateValue().ToString();
            var typeSprite = action.Model.Type switch
            {
                ActionType.Aggression => DataHolder.I.GameData.ActionTypeIcons[action.Model.AggressionType.ToString()],
                ActionType.Defense    => DataHolder.I.GameData.ActionTypeIcons[action.Model.DefenseType.ToString()],
                ActionType.Support    => DataHolder.I.GameData.ActionTypeIcons[action.Model.SupportType.ToString()],
                _ => null
            };
            _magnitudeTypeSprite.sprite = typeSprite;
            if (action.Model.Type != ActionType.Defense)
                _thresholdBlock.SetActive(false);
            else
                _thresholdText.text = action.Threshold.CalculateValue().ToString();
            
            _backgroundSprite.color = DataHolder.I.GameData.ActionTypeColors[action.Model.Type];

            _descriptionText.text = action.Model.RawDescription; // TODO: later use description formatter

            var actionCoinsAmount = action.CoinsAmount.CalculateValue();
            for (int i = 0; i < _coins.Count; i++)
            {
                _coins[i].gameObject.SetActive(actionCoinsAmount > i);
            }

            for (int i = 0; i < action.CoinResults.Count; i++)
            {
                _coins[i].SetState(action.CoinResults[i]);
            }
        }
        
        public void SetRoll(int roll) => _currentRollText.text = roll.ToString();
    }
}