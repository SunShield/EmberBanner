using EmberBanner.Unity.Data.ScriptableObjects;
using EmberBanner.Unity.Data.ScriptableObjects.Databases;
using EmberBanner.Unity.Service;
using UnityEngine;

namespace EmberBanner.Unity.Data
{
    public class DataHolder : EBMonoBehaviour
    {
        private static DataHolder _instance;
        public static DataHolder I
        {
            get
            {
                if (_instance == null)
                    _instance = FindObjectOfType<DataHolder>();
                return _instance;
            }
        }
        
        [SerializeField] private GeneralDatabase _database;
        [SerializeField] private GameData _gameData;
        public GeneralDatabase Databases => _database;
        public GameData GameData => _gameData;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}