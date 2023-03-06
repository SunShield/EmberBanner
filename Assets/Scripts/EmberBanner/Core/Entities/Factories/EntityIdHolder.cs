namespace EmberBanner.Core.Ingame.Factories
{
    public static class EntityIdHolder
    {
        private static int _id;
        public static int GetId => _id++;

        public static void Save()
        {
            
        }

        public static void Load()
        {
            
        }
    }
}