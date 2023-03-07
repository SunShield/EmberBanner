namespace EmberBanner.Core.Entities.Management.SaveLoad.Data
{
    /// <summary>
    /// Minimum data required to restore entity state
    /// </summary>
    public abstract class AbstractEntitySaveData
    {
        public int Id;
        public string ModelName;
        
        public AbstractEntitySaveData() { }
    }
}