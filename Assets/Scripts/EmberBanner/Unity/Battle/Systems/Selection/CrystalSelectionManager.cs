using EmberBanner.Core.Enums.Battle.States;
using EmberBanner.Unity.Battle.Management;
using EmberBanner.Unity.Battle.Systems.CardPlaying.TurnPlanning;
using EmberBanner.Unity.Battle.Views.Impl.Cards;
using EmberBanner.Unity.Battle.Views.Impl.Units.Crystals;
using EmberBanner.Unity.Service;

namespace EmberBanner.Unity.Battle.Systems.Selection
{
    public class CrystalSelectionManager : EBMonoBehaviour
    {
        private static CrystalSelectionManager _instance;
        public static CrystalSelectionManager I
        {
            get
            {
                if (_instance == null)
                    _instance = FindObjectOfType<CrystalSelectionManager>();
                return _instance;
            }
        }
        
        public BattleUnitCrystalView CurrentCrystalWithCard { get; private set; }
        private BattleCardView SelectedCard => CardSelectionManager.I.SelectedCard;

        public void ProcessCrystalClick(BattleUnitCrystalView crystalView)
        {
            if (BattleManager.I.StateController.State != BattleState.TurnPlan) return;

            if (SelectedCard == null)
            {
                if (CurrentCrystalWithCard != null) UnselectCrystal();
                SelectCrystal(crystalView); 
            }
            else
            {
                if (SelectedCard.CanTarget(crystalView))
                {
                    if (SelectedCard.IsLegallyTargetingSelf(crystalView))
                        //CardPrePlayManager.I.SetCardPrePlayed(SelectedCard, CurrentCrystalWithCard);
                        CardPrePlayManager.I.SetCardPrePlayedWithTarget(SelectedCard, CurrentCrystalWithCard, CurrentCrystalWithCard);
                    else
                        CardPrePlayManager.I.SetCardPrePlayedWithTarget(SelectedCard, CurrentCrystalWithCard, crystalView);
                    CardSelectionManager.I.UnselectCard();
                }
                else
                {
                    CardSelectionManager.I.UnselectCard();
                    if (CurrentCrystalWithCard != null) UnselectCrystal();
                    SelectCrystal(crystalView);
                }
            }
        }

        public void SelectCrystal(BattleUnitCrystalView crystalView)
        {
            CurrentCrystalWithCard = crystalView;
            CurrentCrystalWithCard.SetSelected(true);
            UnitSelectionManager.I.SelectSpot(crystalView.OwnerView.Spot);
        }

        public void UnselectCrystal()
        {
            UnitSelectionManager.I.UnselectSpot();
            
            if (CurrentCrystalWithCard == null) return;
            CurrentCrystalWithCard.SetSelected(false);
            CurrentCrystalWithCard = null;
        }
    }
}