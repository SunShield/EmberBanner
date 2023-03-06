using System.Collections.Generic;
using System.Linq;
using EmberBanner.Core.Models.Units;
using EmberBanner.Core.Service.Classes.Collections;
using ItemsManager.Databases;
using UnityEditor;
using UnityEngine;

namespace EmberBanner.Unity.Data.ScriptableObjects.Databases
{
    [CreateAssetMenu(menuName = "Databases/Unit Database", fileName = "UnitDatabase")]
    public class UnitsDatabase : ScriptableObject, IDictionaryDatabase<string, UnitModel>
    {
        [SerializeField] private StringToUnitModelDictionary _cards;
        public IDictionary<string, UnitModel> Elements => _cards;
        public int Count => _cards.Count;
        
        public void Update() => EditorUtility.SetDirty(this);
        public void AddElement(UnitModel element) => _cards.Add(element.Name, element);
        public void RemoveElement(UnitModel element) => _cards.Remove(element.Name);
        public List<UnitModel> GetElementsForIteration() => _cards.Values.ToList();
    }
}