using System;
using EmberBanner.Core.Enums.Battle;
using EmberBanner.Core.Ingame.Impl.Battles;
using EmberBanner.Core.Models.Cards;
using EmberBanner.Unity.Battle.Systems.Selection;
using TMPro;
using UnityEngine;

namespace EmberBanner.Unity.Battle.Views.Impl.Cards
{
    public class BattleCardView : BattleView<BattleCardEntity, CardModel>
    {
        private const float SpotSize = 80f;
        
        [SerializeField] private SpriteRenderer _graphics;
        [SerializeField] private GameObject _selectedGraphics;
        [SerializeField] private TextMeshPro _nameText;
        [SerializeField] private TextMeshPro _costText;

        public BattleCardZone Zone => Entity.Zone;

        protected override void PostInitialize()
        {
            var spriteSize = Entity.Model.Sprite.texture.width;
            var graphicsScale = SpotSize / spriteSize * 2;
            _graphics.sprite = Model.Sprite;_graphics.transform.localScale =
                new Vector3(graphicsScale, graphicsScale, 1f);
            
            _nameText.text   = Model.Name;
            gameObject.name = $"Card [{Entity.Id}, {Model.Name}]";
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

        private void OnMouseDown()
        {
            if (Zone != BattleCardZone.Hand) return;
            
            CardSelectionManager.I.SelectCard(this);
        }

        public void SetSelected(bool selected) => _selectedGraphics.SetActive(selected);
    }
}