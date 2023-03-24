using System.Collections.Generic;
using EmberBanner.Core.Enums.Battle;
using EmberBanner.Core.Ingame.Impl.Battles;
using EmberBanner.Core.Models.Units.Crystals;
using EmberBanner.Unity.Battle.Systems.CardZonesSystem.Zones;
using EmberBanner.Unity.Battle.Systems.Selection;
using EmberBanner.Unity.Battle.Systems.Visuals.CrystalActions;
using EmberBanner.Unity.Battle.Views.Impl.Cards;
using TMPro;
using UnityEngine;

namespace EmberBanner.Unity.Battle.Views.Impl.Units.Crystals
{
    public partial class BattleUnitCrystalView : BattleView<BattleUnitCrystalEntity, UnitCrystalModel>
    {
        [SerializeField] private GameObject _selectedGraphics;
        [SerializeField] private TextMeshPro _rollText;
        [SerializeField] private PlayCardZone _zone;
        [SerializeField] private CrystalActionsUi _actionsUi;
        
        public BattleUnitView OwnerView { get; private set; }
        
        public int? CurrentRoll => Entity.CurrentRoll;
        public UnitControllerType Controller => Entity.Owner.Controller;
        public PlayCardZone Zone => _zone;
        public BattleCardView Card => _zone.Cards.Count > 0 ? _zone.Cards[0] : null;
        public bool IsEnemy => Controller == UnitControllerType.Enemy;
        public bool IsStaggered => OwnerView.IsStaggered;
        public bool IsDead => OwnerView.IsDead;

        public List<BattlePlayingActionEntity> Actions { get; private set; } = new();
        private int _nonCancelledActionsCount;
        public bool HasNonCancelledActions => _nonCancelledActionsCount > 0;
        
        public void SetOwnerView(BattleUnitView ownerView) => OwnerView = ownerView;

        public void Roll()
        {
            Entity.DoRoll();
            _rollText.text = CurrentRoll.ToString();
        }

        private void OnMouseDown()
        {
            CrystalSelectionManager.I.ProcessCrystalClick(this);
        }

        public void SetSelected(bool selected) => _selectedGraphics.SetActive(selected);

        public void SetCardPrePlayed(BattleCardView card)
        {
            if (Card != null) UnsetCardPrePlayed(card);
            OwnerView.SetCardPrePlayed(card, this);
        }

        public void UnsetCardPrePlayed(BattleCardView card)
        {
            if (Card == null) return;
            OwnerView.UnsetCardPrePlayed(card, this);
        }

        // will be used later
        public bool CanBeTargeted(BattleCardView card) => true;

        public void AddActions(List<BattlePlayingActionEntity> actions)
        {
            Actions = actions;
            _actionsUi.SetActive(true);
            _actionsUi.AddActions(Actions);
        }

        public void OnTurnEnd()
        {
            _rollText.text = "X";
            _actionsUi.SetActive(false);
            _actionsUi.ClearActions();
            Actions = new();
            _nonCancelledActionsCount = 0;
        }

        public void SetNonCancelledActionsAmount(int nonCancelledActionsAmount) =>
            _nonCancelledActionsCount = nonCancelledActionsAmount;
        
        public void PickAction(BattlePlayingActionEntity action)
        {
            _nonCancelledActionsCount--;
            Actions.Remove(action);
        }
    }
}