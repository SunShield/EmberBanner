using System.Collections.Generic;
using EmberBanner.Core.Enums.Battle;
using EmberBanner.Unity.Battle.Views.Impl.Cards;
using EmberBanner.Unity.Service;
using UnityEngine;

namespace EmberBanner.Unity.Battle.Systems.CardZonesSystem.Zones
{
    public abstract class AbstractCardZone : EBMonoBehaviour
    {
        [SerializeField] protected Transform CardsOrigin;
        public abstract BattleCardZone Type { get; }

        public List<BattleCardView> Cards { get; private set; } = new();
        public int Count => Cards.Count;

        public void AddCard(BattleCardView card)
        {
            Cards.Add(card);
            DoAddCard(card);
            card.Entity.SetZone(Type);
            card.OnEnterZone(Type);
        }
        
        protected abstract void DoAddCard(BattleCardView card);

        public void RemoveCard(BattleCardView card)
        {
            card.OnLeaveZone(Type);
            Cards.Remove(card);
            DoRemoveCard(card);
        }

        public BattleCardView RemoveCard(int index)
        {
            var card = Cards[index];
            RemoveCard(card);
            return card;
        }
        
        protected virtual void DoRemoveCard(BattleCardView card) { }
    }
}