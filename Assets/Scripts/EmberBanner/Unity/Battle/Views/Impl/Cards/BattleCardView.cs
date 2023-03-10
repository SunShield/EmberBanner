using EmberBanner.Core.Enums.Battle;
using EmberBanner.Core.Ingame.Impl.Battles;
using EmberBanner.Core.Models.Cards;
using EmberBanner.Unity.Battle.Management;
using EmberBanner.Unity.Battle.Systems.Selection;
using EmberBanner.Unity.Battle.Views.Impl.Units;
using EmberBanner.Unity.Battle.Views.Impl.Units.Crystals;
using TMPro;
using UnityEngine;
using BattleUnitCrystalView = EmberBanner.Unity.Battle.Views.Impl.Units.Crystals.BattleUnitCrystalView;

namespace EmberBanner.Unity.Battle.Views.Impl.Cards
{
    public class BattleCardView : BattleView<BattleCardEntity, CardModel>
    {
        private const float SpotSize = 80f;
        
        [SerializeField] private SpriteRenderer _graphics;
        [SerializeField] private GameObject _selectedGraphics;
        [SerializeField] private TextMeshPro _nameText;
        [SerializeField] private TextMeshPro _costText;

        public BattleUnitView Owner { get; private set; }
        public BattleUnitCrystalView Crystal { get; private set; }

        public BattleCardZone Zone => Entity.Zone;
        protected override void PostInitialize()
        {
            var spriteSize = Entity.Model.Sprite.texture.width;
            var graphicsScale = SpotSize / spriteSize * 2;
            _graphics.sprite = Model.Sprite;_graphics.transform.localScale =
                new Vector3(graphicsScale, graphicsScale, 1f);
            
            _nameText.text   = Model.Name;
            gameObject.name = $"Card [{Entity.Id}, {Model.Name}]";

            Owner = BattleManager.I.Registry.Units[Entity.Owner.Id];
        }

        public void OnLeaveZone(BattleCardZone zone)
        {
            if (zone == BattleCardZone.Play)
            {
                gameObject.SetActive(true);
            }
        }

        public void OnEnterZone(BattleCardZone zone)
        {
            if (zone == BattleCardZone.Hand)
            {
                _costText.text = Model.Cost.ToString();
            }
            else if (zone == BattleCardZone.Play)
            {
                gameObject.SetActive(false);
            }
        }

        private void OnMouseDown()
        {
            if (Zone != BattleCardZone.Hand) return;
            
            CardSelectionManager.I.SelectCard(this);
        }

        public void SetSelected(bool selected) => _selectedGraphics.SetActive(selected);

        public bool CanBePlayed() => Owner.CanPlayCard(this);

        public bool CanTarget(BattleUnitCrystalView potentialTarget)
        {
            return potentialTarget.OwnerView != Owner && // dont target self
                   ((potentialTarget.Controller == UnitControllerType.Enemy && Model.MainTarget == CardMainTargetType.Enemy) ||
                   (potentialTarget.Controller == UnitControllerType.Player && Model.MainTarget == CardMainTargetType.Ally));
        }

        public void SetPrePlayed(BattleUnitCrystalView crystal)
        {
            Owner.PayCard(this);
            Crystal = crystal;
            crystal.SetCardPrePlayed(this);
        }

        public void UnsetPrePlayed()
        {
            Owner.UnpayCard(this);
            Crystal.UnsetCardPrePlayed(this);
            Crystal = null;
        }
    }
}