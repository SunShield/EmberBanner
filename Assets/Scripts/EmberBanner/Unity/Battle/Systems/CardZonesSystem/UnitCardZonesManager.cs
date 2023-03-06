using EmberBanner.Unity.Battle.Systems.CardZonesSystem.Zones;
using EmberBanner.Unity.Battle.Views.Units;
using EmberBanner.Unity.Service;
using UnityEngine;

namespace EmberBanner.Unity.Battle.Systems.CardZonesSystem
{
    public class UnitCardZonesManager : EBMonoBehaviour
    {
        [SerializeField] private LibraryCardZone _library;
        [SerializeField] private HandCardZone _hand;
        [SerializeField] private GraveyardCardZone _graveyard;

        private BattleUnitView _owner;

        public void Initialize(BattleUnitView owner)
        {
            _owner = owner;
        }
    }
}