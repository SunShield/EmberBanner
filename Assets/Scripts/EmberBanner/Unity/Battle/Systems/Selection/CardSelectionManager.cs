using EmberBanner.Core.Enums.Battle;
using EmberBanner.Core.Enums.Battle.States;
using EmberBanner.Core.Enums.Battle.Targeting;
using EmberBanner.Unity.Battle.Management;
using EmberBanner.Unity.Battle.Systems.CardPlaying.TurnPlanning;
using EmberBanner.Unity.Battle.Views.Impl.Cards;
using EmberBanner.Unity.Service;

namespace EmberBanner.Unity.Battle.Systems.Selection
{
    public class CardSelectionManager : EBMonoBehaviour
    {
        private static CardSelectionManager _instance;
        public static CardSelectionManager I
        {
            get
            {
                if (_instance == null)
                    _instance = FindObjectOfType<CardSelectionManager>();
                return _instance;
            }
        }
        
        public BattleCardView SelectedCard { get; private set; }

        public void SelectCard(BattleCardView card)
        {
            if (BattleManager.I.StateController.State != BattleState.TurnPlan) return;
            if (card.Owner.Entity.Controller == UnitControllerType.Enemy) return;
            
            if (SelectedCard != null) return;

            if (card.Entity.Model.TargetType == TargetType.Self)
            {
                if (!card.CanBePlayed()) return;
                
                CardPrePlayManager.I.SetCardPrePlayed(card, CrystalSelectionManager.I.CurrentCrystalWithCard);
            }
            else
            {
                SelectedCard = card;
                SelectedCard.SetSelected(true);
            }
        }

        public void UnselectCard()
        {
            SelectedCard.SetSelected(false);
            SelectedCard = null;
        }
    }
}