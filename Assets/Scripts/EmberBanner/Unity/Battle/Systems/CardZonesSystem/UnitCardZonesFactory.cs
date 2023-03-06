using EmberBanner.Unity.Service;
using UnityEngine;

namespace EmberBanner.Unity.Battle.Systems.CardZonesSystem
{
    public class UnitCardZonesFactory : EBMonoBehaviour
    {
        [SerializeField] private Transform _zonesOrigin;
        [SerializeField] private UnitCardZonesManager _zonesPrefab;
    }
}