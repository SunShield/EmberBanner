using EmberBanner.Core.Enums.Battle;
using EmberBanner.Core.Ingame.Impl.Battles;
using EmberBanner.Core.Models.Units.Crystals;
using TMPro;
using UnityEngine;

namespace EmberBanner.Unity.Battle.Views.Impl.Units.Crystals
{
    public class BattleUnitCrystalView : BattleView<BattleUnitCrystalEntity, UnitCrystalModel>
    {
        [SerializeField] private TextMeshPro _rollText;
        
        public int? CurrentRoll => Entity.CurrentRoll;
        public UnitControllerType Controller => Entity.Owner.Controller;

        public void Roll()
        {
            Entity.DoRoll();
            _rollText.text = CurrentRoll.ToString();
        }

        public void Clear()
        {
            _rollText.text = "X";
        }
    }
}