using EmberBanner.Core.Enums.Actions;
using EmberBanner.Core.Ingame.Impl.Battles;
using EmberBanner.Unity.Data;
using EmberBanner.Unity.Service;
using TMPro;
using UnityEngine;

namespace EmberBanner.Unity.Battle.Systems.Visuals.ActionsResolve
{
    public class ResolvingActionUi : EBMonoBehaviour
    {
        [SerializeField] private SpriteRenderer _backgroundSprite;
        [SerializeField] private TextMeshPro _magnitudeRoll;
        [SerializeField] private SpriteRenderer _magnitudeTypeSprite;
        [SerializeField] private TextMeshPro _coinsText;
        [SerializeField] private TextMeshPro _rollBoundsText;
        [SerializeField] private GameObject _thresholdBlock;
        [SerializeField] private TextMeshPro _thresholdText;

        public void SetAction(BattlePlayingActionEntity action)
        {
            _magnitudeRoll.text = action.Magnitude.CalculateValue().ToString();
            
            var typeSprite = action.Model.Type switch
            {
                ActionType.Aggression => DataHolder.I.GameData.ActionTypeIcons[action.Model.AggressionType.ToString()],
                ActionType.Defense    => DataHolder.I.GameData.ActionTypeIcons[action.Model.DefenseType.ToString()],
                ActionType.Support    => DataHolder.I.GameData.ActionTypeIcons[action.Model.SupportType.ToString()],
                _ => null
            };
            _magnitudeTypeSprite.sprite = typeSprite;
            
            _coinsText.text = (action.CoinsAmount.CalculateValue() - action.CoinResults.Count).ToString();
            _rollBoundsText.text = $"{action.MinClashingPower.CalculateValue()}~{action.MaxClashingPower.CalculateValue()}";
            if (action.Model.Type != ActionType.Defense)
                _thresholdBlock.SetActive(false);
            else
                _thresholdText.text = action.Threshold.CalculateValue().ToString();
            
            _backgroundSprite.color = DataHolder.I.GameData.ActionTypeColors[action.Model.Type];

            if (action.IsCancelled) SetActionCancelled();
        }

        private void SetActionCancelled()
        {
            _magnitudeRoll.gameObject.SetActive(false);
            _coinsText.gameObject.SetActive(false);
            _rollBoundsText.gameObject.SetActive(false);
            _thresholdText.gameObject.SetActive(false);
        }
    }
}