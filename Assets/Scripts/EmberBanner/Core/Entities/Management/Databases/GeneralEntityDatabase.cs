using EmberBanner.Core.Entities.Management.Databases.Impl;

namespace EmberBanner.Core.Entities.Management.Databases
{
    public class GeneralEntityDatabase
    {
        private static GeneralEntityDatabase _instance;
        public static GeneralEntityDatabase I => _instance ??= new();

        protected GeneralEntityDatabase()
        {
            Load();
        }

        public CardEntitiesDatabase Cards = new();
        public UnitEntitiesDatabase Units = new();

        public void Save()
        {
            Cards.Save();
            Units.Save();
        }

        public void Load()
        {
            Cards.Load();
            Units.Load();
        }
    }
}