using UnityEngine;

namespace EmberBanner.Core.Entities.Management.Factories
{
    public static class EntityIdHolder
    {
        private static int _id;
        public static int GetId => _id++;

        public static void Save() => PlayerPrefs.SetInt("CurrentEntityKey", _id);
        public static void Load() => _id = PlayerPrefs.GetInt("CurrentEntityKey");
    }
}