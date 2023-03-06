using ItemsManager.Databases;
using UnityEngine;

namespace EmberBanner.Unity.Data.ScriptableObjects.Databases
{
    [CreateAssetMenu(menuName = "Databases/General Database", fileName = "GeneralDatabase")]
    public class GeneralDatabase : AbstractGeneralDatabase
    {
#if UNITY_EDITOR

        private static GeneralDatabase _editorInstance;

        public static GeneralDatabase EI
        {
            get
            {
                if (_editorInstance == null)
                    _editorInstance = UnityEditor.AssetDatabase.LoadAssetAtPath<GeneralDatabase>(@"Assets/Data/Databases/GeneralDatabase.asset");
                return _editorInstance;
            }
        }
        
#endif
        
        public CardsDatabase Cards;
        public UnitsDatabase Units;
        public BattlesDatabase Battles;
    }
}