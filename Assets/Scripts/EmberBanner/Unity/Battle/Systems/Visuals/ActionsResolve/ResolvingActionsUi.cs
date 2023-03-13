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
            _crystal = crystal;
            for (int i = 0; i < _actions.Count; i++)
            {
                var crystalActionsCount = crystal.Actions.Count;
                if (i >= crystal.Actions.Count)
                    _actions[i].gameObject.SetActive(false);
                else
                    _actions[i].SetAction(crystal.Actions[i]);
            }
        }
    }
}