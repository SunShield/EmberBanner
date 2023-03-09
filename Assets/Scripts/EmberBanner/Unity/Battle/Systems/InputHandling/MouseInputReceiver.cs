using EmberBanner.Unity.Battle.Systems.CardPlaying.PrePlaying;
using EmberBanner.Unity.Battle.Systems.Selection;
using EmberBanner.Unity.Service;
using UnityEngine;

namespace EmberBanner.Unity.Battle.Systems.InputHandling
{
    public class MouseInputReceiver : EBMonoBehaviour
    {
        private void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                if (CardSelectionManager.I.SelectedCard != null)
                {
                    CardSelectionManager.I.UnselectCard();
                }
                else if (CrystalSelectionManager.I.CurrentCrystalWithCard != null &&
                         CrystalSelectionManager.I.CurrentCrystalWithCard.Card != null)
                         CardPrePlayManager.I.UnsetCardPrePlayed(CrystalSelectionManager.I.CurrentCrystalWithCard.Card);
                else if (CrystalSelectionManager.I.CurrentCrystalWithCard != null)
                {
                    CrystalSelectionManager.I.UnselectCrystal();
                }
            }
        }
    }
}