using System.Collections.Generic;
using EmberBanner.Unity.Battle.Views.Cards;
using EmberBanner.Unity.Service;
using UnityEngine;

namespace EmberBanner.Unity.Battle.Systems.CardZonesSystem.Zones
{
    public abstract class AbstractCardZone : EBMonoBehaviour
    {
        [SerializeField] protected Transform CardsOrigin;
        protected List<BattleCardView> Cards { get; private set; }

        public void AddCard(BattleCardView card)
        {
            Cards.Add(card);
            DoAddCard(card);
            card.EnterZone();
        }
        
        protected abstract void DoAddCard(BattleCardView card);

        public void RemoveCard(BattleCardView card)
        {
            card.LeaveZone();
            Cards.Remove(card);
            DoRemoveCard(card);
        }
        
        protected virtual void DoRemoveCard(BattleCardView card) { }
    }
}