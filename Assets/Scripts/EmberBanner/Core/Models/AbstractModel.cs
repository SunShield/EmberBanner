using System;
using ItemsManager.Databases.Elements;
using UnityEngine;

namespace EmberBanner.Core.Models
{
    /// <summary>
    /// Models are classes holding persistant data
    /// 
    /// They never change and can be edited through editor tools
    /// </summary>
    [Serializable]
    public abstract class AbstractModel : IAbstractDatabaseElement
    {
        [field: SerializeField] public string Name { get; set; }
    }
}