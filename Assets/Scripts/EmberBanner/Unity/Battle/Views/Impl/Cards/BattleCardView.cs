using EmberBanner.Core.Entities.Impl.Cards;
using EmberBanner.Core.Enums.Battle;
using EmberBanner.Core.Ingame.Impl.Battles;
using EmberBanner.Core.Models.Cards;
using TMPro;
using UnityEngine;

namespace EmberBanner.Unity.Battle.Views.Impl.Cards
{
    public class BattleCardView : BattleView<BattleCardEntity, CardModel>
    {
        [SerializeField] private SpriteRenderer _graphics;
        [SerializeField] private TextMeshPro _nameText;
        [SerializeField] private TextMeshPro _costText;

        public BattleCardZone Zone => Entity.Zone;

        protected override void PostInitialize()
        {
            _graphics.sprite = Model.Sprite;
            _nameText.text   = Model.Name;
        }

        public void OnLeaveZone(BattleCardZone zone)
        {
            
        }

        public void OnEnterZone(BattleCardZone zone)
        {
            if (zone == BattleCardZone.Hand)
            {
                _costText.text = Model.Cost.ToString();
            }
        }
    }
}