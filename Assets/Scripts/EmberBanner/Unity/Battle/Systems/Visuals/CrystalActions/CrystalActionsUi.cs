using System.Collections.Generic;
using EmberBanner.Core.Ingame.Impl.Battles;
using EmberBanner.Unity.Battle.Systems.Visuals.PrePlayedCards;
using EmberBanner.Unity.Service;
using UnityEngine;

namespace EmberBanner.Unity.Battle.Systems.Visuals.CrystalActions
{
    public class CrystalActionsUi : EBMonoBehaviour
    {
        private const float ActionsTopBottomPadding = 0.05f;
        private const float ActionsMargin = 0.05f;
        private const float ActionHeight = 0.15f;
        
        [SerializeField] private Transform _actionsOrigin;
        [SerializeField] private PrePlayedCardActionUi _actionUiPrefab;
        [SerializeField] private List<PrePlayedCardActionUi> _actionUis = new();
        [SerializeField] private SpriteRenderer _bg;
        [SerializeField] private SpriteRenderer _frame;

        public void SetActive(bool active) => gameObject.SetActive(active);

        public void AddActions(List<BattlePlayingActionEntity> actions)
        {
            var actionsCount = actions.Count;
            var height = ActionsTopBottomPadding * 2 + ActionHeight * actionsCount + ActionsMargin * (actionsCount - 1);
            _bg.size = new Vector2(_bg.size.x, height);
            _frame.size = new Vector2(_frame.size.x, height * 4); // because default frame size is 0.25

            _actionsOrigin.transform.localPosition = new Vector3(0f, height, 0f);

            foreach (var actionEntity in actions)
            {
                var actionUi = Instantiate(_actionUiPrefab);
                actionUi.Initialize(actionEntity);
                actionUi.transform.parent = _actionsOrigin;
                _actionUis.Add(actionUi);
                if (actionEntity.IsCancelled) actionUi.SetCancelled(true);
            }

            for (int i = 0; i < actions.Count; i++)
            {
                var actionUi = _actionUis[i];
                var actionFullHeight = ActionHeight + ActionsMargin;
                var y = -ActionsTopBottomPadding - ActionHeight - (ActionHeight + ActionsMargin) * i; 
                actionUi.transform.localPosition = new Vector3(0f, y, 0f);
            }
        }

        public void ClearActions()
        {
            foreach (var cardActionUi in _actionUis)
            {
                Destroy(cardActionUi.gameObject);
            }
            _actionUis.Clear();
        }
    }
}