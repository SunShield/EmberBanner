using System;

namespace EmberBanner.Core.Models
{
    /// <summary>
    /// Models are classes holding persistant data
    /// 
    /// They never change and can be edited through editor tools
    /// </summary>
    [Serializable]
    public abstract class AbstractModel
    {
        public string Name;
    }
}