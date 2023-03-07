using UnityEngine;

namespace EmberBanner.Core.Entities.Management.Factories
{
    public static class EntityIdHolder
    {
        private const int NoId = -1;
        
        private static int _id = -1;
        public static int GetId
        {
            get
            {
                if (_id == NoId) Load();
                _id++;
                Save();
                return _id;
            }
        }

        private static void Save() => PlayerPrefs.SetInt("CurrentEntityKey", _id);
        private static void Load()
        {
            if (!PlayerPrefs.HasKey("CurrentEntityKey")) return;
            
            _id = PlayerPrefs.GetInt("CurrentEntityKey");
        }
    }
}