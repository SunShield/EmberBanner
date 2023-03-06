using ItemsManager.Databases;
using UnityEngine;

namespace EmberBanner.Unity.Data.ScriptableObjects.Databases
{
    [CreateAssetMenu(menuName = "Databases/General Database", fileName = "GeneralDatabase")]
    public class GeneralDatabase : AbstractGeneralDatabase
    {
        public CardsDatabase Cards;
        public UnitsDatabase Units;
        public BattlesDatabase Battles;
    }
}