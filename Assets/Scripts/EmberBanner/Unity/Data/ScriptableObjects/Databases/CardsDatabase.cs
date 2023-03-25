using System.Collections.Generic;
using System.Linq;
using EmberBanner.Core.Models.Cards;
using EmberBanner.Core.Service.Classes.Collections;
using ItemsManager.Databases;
using UnityEditor;
using UnityEngine;

namespace EmberBanner.Unity.Data.ScriptableObjects.Databases
{
    [CreateAssetMenu(menuName = "EB/Databases/Card Database", fileName = "CardDatabase")]
    public class CardsDatabase : ScriptableObject, IDictionaryDatabase<string, CardModel>
    {
        [SerializeField] private StringToCardModelDictionary _cards;
        public IDictionary<string, CardModel> Elements => _cards;
        public int Count => _cards.Count;
        
        public void Update() => EditorUtility.SetDirty(this);
        public void AddElement(CardModel element) => _cards.Add(element.Name, element);
        public void RemoveElement(CardModel element) => _cards.Remove(element.Name);
        public List<CardModel> GetElementsForIteration() => _cards.Values.ToList();
        public CardModel this[string name] => Elements[name];
    }
}