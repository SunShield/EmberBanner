using EmberBanner.Unity.Battle.Views.Factories.Impl;
using EmberBanner.Unity.Battle.Views.Impl.Units;
using EmberBanner.Unity.Service;
using UnityEngine;

namespace EmberBanner.Unity.Battle.Systems.CardZonesSystem
{
    public class UnitCardZonesFactory : EBMonoBehaviour
    {
        private static UnitCardZonesFactory _instance;
        public static UnitCardZonesFactory I
        {
            get
            {
                if (_instance == null)
                    _instance = FindObjectOfType<UnitCardZonesFactory>();
                return _instance;
            }
        }
        
        [SerializeField] private Transform _zonesOrigin;
        [SerializeField] private UnitCardZonesManager _zonesPrefab;

        public void CreateCardZones(BattleUnitView unit)
        {
            var zonesManager = Instantiate(_zonesPrefab, _zonesOrigin.transform.position, Quaternion.identity, _zonesOrigin);
            zonesManager.gameObject.SetActive(false);
            zonesManager.Initialize(unit);
            unit.SetZonesManager(zonesManager);

            foreach (var cardEntity in unit.Entity.EnumerateCards())
            {
                var cardView = BattleCardViewFactory.I.CreateView(cardEntity);
            }
        }
    }
}