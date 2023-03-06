using System.Collections.Generic;
using System.Linq;
using EmberBanner.Core.Models.Units;
using EmberBanner.Core.Service.Classes.Collections;
using ItemsManager.Databases;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace EmberBanner.Unity.Data.ScriptableObjects.Databases
{
    [CreateAssetMenu(menuName = "Databases/Unit Database", fileName = "UnitDatabase")]
    public class UnitsDatabase : ScriptableObject, IDictionaryDatabase<string, UnitModel>
    {
        [FormerlySerializedAs("_cards")] [SerializeField] private StringToUnitModelDictionary _units;
        public IDictionary<string, UnitModel> Elements => _units;
        public int Count => _units.Count;
        
        public void Update() => EditorUtility.SetDirty(this);
        public void AddElement(UnitModel element) => _units.Add(element.Name, element);
        public void RemoveElement(UnitModel element) => _units.Remove(element.Name);
        public List<UnitModel> GetElementsForIteration() => _units.Values.ToList();
    }
}