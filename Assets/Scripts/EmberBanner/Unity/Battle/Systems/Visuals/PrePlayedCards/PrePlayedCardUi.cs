using System.Collections.Generic;
using EmberBanner.Unity.Battle.Views.Impl.Units.Crystals;
using EmberBanner.Unity.Service;
using UnityEngine;

namespace EmberBanner.Unity.Battle.Systems.Visuals.PrePlayedCards
{
    public class PrePlayedCardUi : EBMonoBehaviour
    {
        [SerializeField] private List<PrePlayedCardActionUi> _actionUis;

        public void Initialize(BattleUnitCrystalView crystal)
        {
            for (int i = 0; i < _actionUis.Count; i++)
            {
                if (crystal.Card.Entity.Actions.Count > i)
                {
                    _actionUis[i].Initialize(crystal.Card.Entity.Actions[i]);
                }
                else
                {
                    _actionUis[i].gameObject.SetActive(false);
                }
            }
        }
    }
}