using System.Collections.Generic;
using System.Linq;
using EmberBanner.Core.Models.Battles;
using EmberBanner.Core.Service.Classes.Collections;
using ItemsManager.Databases;
using UnityEditor;
using UnityEngine;

namespace EmberBanner.Unity.Data.ScriptableObjects.Databases
{
    [CreateAssetMenu(menuName = "Databases/Battle Database", fileName = "BattleDatabase")]
    public class BattlesDatabase : ScriptableObject, IDictionaryDatabase<string, BattleModel>
    {
        [SerializeField] private StringToBattleModelDictionary _cards;
        public IDictionary<string, BattleModel> Elements => _cards;
        public int Count => _cards.Count;
        
        public void Update() => EditorUtility.SetDirty(this);
        public void AddElement(BattleModel element) => _cards.Add(element.Name, element);
        public void RemoveElement(BattleModel element) => _cards.Remove(element.Name);
        public List<BattleModel> GetElementsForIteration() => _cards.Values.ToList();
    }
}