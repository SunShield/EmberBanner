﻿using EmberBanner.Core.Enums.Battle;
using EmberBanner.Unity.Battle.Views.Impl.Cards;
using UnityEngine;

namespace EmberBanner.Unity.Battle.Systems.CardZonesSystem.Zones
{
    public class LibraryCardZone : AbstractCardZone
    {
        public override BattleCardZone Type { get; } = BattleCardZone.Library;

        protected  override void DoAddCard(BattleCardView card)
        {
            card.gameObject.SetActive(false);
            card.Tran.SetParent(CardsOrigin);
            card.Tran.localPosition = Vector3.zero;
        }
    }
}