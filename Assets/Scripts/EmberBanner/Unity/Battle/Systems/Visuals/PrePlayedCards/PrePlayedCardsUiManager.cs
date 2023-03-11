using System.Collections.Generic;
using EmberBanner.Unity.Battle.Systems.CardPlaying.TurnPlanning;
using EmberBanner.Unity.Battle.Views.Factories.Impl;
using EmberBanner.Unity.Battle.Views.Impl.Units.Crystals;
using EmberBanner.Unity.Service;
using UnityEngine;

namespace EmberBanner.Unity.Battle.Systems.Visuals.PrePlayedCards
{
    public class PrePlayedCardsUiManager : EBMonoBehaviour
    {
        private static PrePlayedCardsUiManager _instance;
        public static PrePlayedCardsUiManager I
        {
            get
            {
                if (_instance == null) _instance = FindObjectOfType<PrePlayedCardsUiManager>();
                return _instance;
            }
        }

        [SerializeField] private Transform _uisOrigin;
        [SerializeField] private PrePlayedCardUi _prefab;

        private Dictionary<BattleUnitCrystalView, PrePlayedCardUi> _crystalsToUiMap = new();

        private void Awake()
        {
            CardPrePlayManager.I.onCardSetPrePlay += SpawnPrePlayerUi;
            CardPrePlayManager.I.onCardUnsetPrePlay += RemoveUi;
        }

        public void SpawnPrePlayerUi(BattleUnitCrystalView crystal)
        {
            var ui = InstantiateUi(crystal.Tran.position);
            ui.Initialize(crystal);
            _crystalsToUiMap.Add(crystal, ui);
        }

        public void RemoveUi(BattleUnitCrystalView crystal)
        {
            Destroy(_crystalsToUiMap[crystal].gameObject);
            _crystalsToUiMap.Remove(crystal);
        }

        public void Clear()
        {
            foreach (var cardUi in _crystalsToUiMap.Values)
            {
                Destroy(cardUi.gameObject);
            }
            
            _crystalsToUiMap.Clear();
        }

        private PrePlayedCardUi InstantiateUi(Vector3 position) => Instantiate(_prefab, position + new Vector3(0f, 0.6f, 0f), Quaternion.identity, _uisOrigin); 
    }
}