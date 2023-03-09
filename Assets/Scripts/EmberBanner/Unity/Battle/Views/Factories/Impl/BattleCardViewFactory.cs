using EmberBanner.Core.Ingame.Impl.Battles;
using EmberBanner.Core.Models.Cards;
using EmberBanner.Unity.Battle.Views.Impl.Cards;
using EmberBanner.Unity.Views;
using UnityEngine;

namespace EmberBanner.Unity.Battle.Views.Factories.Impl
{
    public class BattleCardViewFactory : ViewFactory<BattleCardView, BattleCardEntity, CardModel>
    {
        private static BattleCardViewFactory _instance;
        public static BattleCardViewFactory I
        {
            get
            {
                if (_instance == null) _instance = FindObjectOfType<BattleCardViewFactory>();
                return _instance;
            }
        }
        
        [SerializeField] private BattleCardView _prefab;

        protected override BattleCardView GetPrefab(BattleCardEntity entity) => _prefab;
    }
}