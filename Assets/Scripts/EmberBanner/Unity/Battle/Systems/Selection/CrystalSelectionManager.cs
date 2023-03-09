using EmberBanner.Unity.Battle.Systems.CardPlaying.PrePlaying;
using EmberBanner.Unity.Battle.Views.Impl.Cards;
using EmberBanner.Unity.Battle.Views.Impl.Units.Crystals;
using EmberBanner.Unity.Service;

namespace EmberBanner.Unity.Battle.Systems.Selection
{
    public class CrystalSelectionManager : EBMonoBehaviour
    {
        private BattleUnitCrystalView _currentCrystalWithCard;
        private BattleCardView SelectedCard => CardSelectionManager.I.SelectedCard;
        
        public void SelectCrystal(BattleUnitCrystalView crystalView)
        {
            if (_currentCrystalWithCard != null && SelectedCard != null)
            {
                CardPrePlayManager.I.TryAddCardToCardTargetMatrix(SelectedCard, crystalView);
            }
            else
            {
                _currentCrystalWithCard = crystalView;
            }
        }
    }
}