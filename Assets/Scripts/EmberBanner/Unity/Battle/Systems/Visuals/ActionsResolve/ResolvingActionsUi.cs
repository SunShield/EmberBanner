using System.Collections.Generic;
using EmberBanner.Unity.Battle.Views.Impl.Units.Crystals;
using EmberBanner.Unity.Service;
using UnityEngine;

namespace EmberBanner.Unity.Battle.Systems.Visuals.ActionsResolve
{
    public class ResolvingActionsUi : EBMonoBehaviour
    {
        [SerializeField] private List<ResolvingActionUi> _actions;
        private BattleUnitCrystalView _crystal;
        
        public void SetActions(BattleUnitCrystalView crystal)
        {
            gameObject.SetActive(true);
            _crystal = crystal;
            for (int i = 0; i < _actions.Count; i++)
            {
                _actions[i].gameObject.SetActive(i < crystal.Actions.Count);
                if (i < crystal.Actions.Count)
                    _actions[i].SetAction(crystal.Actions[i]);
            }
        }

        public void Clear()
        {
            _crystal = null;
            foreach (var action in _actions)
            {
                action.gameObject.SetActive(false);
            }
            gameObject.SetActive(false);
        }
    }
}