﻿using EmberBanner.Core.Entities.Impl.Cards;
using EmberBanner.Core.Enums.Battle;
using EmberBanner.Core.Models.Cards;

namespace EmberBanner.Unity.Battle.Views.Impl.Cards
{
    public class BattleCardView : BattleView<CardEntity, CardModel>
    {
        public BattleCardZone Zone { get; private set; }

        public void LeaveZone()
        {
            
        }

        public void EnterZone()
        {
            
        }
    }
}