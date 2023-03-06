using System.Collections.Generic;
using ItemsManager.Databases;
using UnityEditor;
using UnityEngine;

namespace ItemsManager.Test.Databases
{
    [CreateAssetMenu(menuName = "ItemsManager/Create Test List Db", fileName = "TestListDb")]
    public class TestListDatabase : ScriptableObject, IListDatabase<TestListAbstractDatabaseElement>
    {
        public List<TestListAbstractDatabaseElement> elements;
        public List<TestListAbstractDatabaseElement> Elements => elements;

        public int Count => Elements.Count;

        public void Update()
        {
            EditorUtility.SetDirty(this);
        }

        public void AddElement(TestListAbstractDatabaseElement element) => elements.Add(element);
        public void RemoveElement(TestListAbstractDatabaseElement element) => elements.Remove(element);
        public List<TestListAbstractDatabaseElement> GetElementsForIteration() => elements;
    }
}