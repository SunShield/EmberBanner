using System.Collections.Generic;
using System.Linq;
using EmberBanner.Core.Models.Battles;
using EmberBanner.Core.Service.Classes.Collections;
using ItemsManager.Databases;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace EmberBanner.Unity.Data.ScriptableObjects.Databases
{
    [CreateAssetMenu(menuName = "EB/Databases/Battle Database", fileName = "BattleDatabase")]
    public class BattlesDatabase : ScriptableObject, IDictionaryDatabase<string, BattleModel>
    {
        [FormerlySerializedAs("_cards")] [SerializeField] private StringToBattleModelDictionary _battles;
        public IDictionary<string, BattleModel> Elements => _battles;
        public int Count => _battles.Count;
        
        public void Update() => EditorUtility.SetDirty(this);
        public void AddElement(BattleModel element) => _battles.Add(element.Name, element);
        public void RemoveElement(BattleModel element) => _battles.Remove(element.Name);
        public List<BattleModel> GetElementsForIteration() => _battles.Values.ToList();
        public BattleModel this[string name] => Elements[name];
    }
}