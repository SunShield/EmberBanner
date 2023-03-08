﻿using System.Collections.Generic;
using EmberBanner.Core.Enums.Battle;
using EmberBanner.Core.Ingame.Impl.Battles;
using EmberBanner.Core.Models.Units;
using EmberBanner.Unity.Battle.Systems.CardZonesSystem;
using EmberBanner.Unity.Battle.Views.Impl.Units.Crystals;
using UnityEngine;

namespace EmberBanner.Unity.Battle.Views.Impl.Units
{
    public class BattleUnitView : BattleView<BattleUnitEntity, UnitModel>
    {
        private const float SpotSize = 64f;
        
        [SerializeField] private SpriteRenderer _graphics;
        [SerializeField] private BattleUnitCrystalsView _unitCrystals;
        private UnitCardZonesManager _zonesManager;

        public UnitControllerType Controller => Entity.Controller;
        public BattleUnitCrystalsView UnitCrystals => _unitCrystals;

        public void SetCrystals(List<BattleUnitCrystalView> crystals) => _unitCrystals.SetCrystals(crystals);

        protected override void PostInitialize()
        {
            SetGraphics();
        }

        private void SetGraphics()
        {
            _graphics.sprite = Entity.Model.Sprite;
            var spriteSize = Entity.Model.Sprite.texture.width;
            var graphicsScale = SpotSize / spriteSize * 2;
            _graphics.transform.localScale =
                new Vector3((Controller == UnitControllerType.Player ? 1f : -1f) * graphicsScale, graphicsScale, 1f);
        }
    }
}