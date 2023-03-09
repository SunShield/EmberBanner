using EmberBanner.Core.Enums.Battle.States;
using EmberBanner.Unity.Battle.Management;
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
            
            if (SelectedCard != null) UnselectCard();
            SelectedCard = card;
            SelectedCard.SetSelected(true);
        }

        public void UnselectCard()
        {
            SelectedCard.SetSelected(false);
            SelectedCard = null;
        }
    }
}