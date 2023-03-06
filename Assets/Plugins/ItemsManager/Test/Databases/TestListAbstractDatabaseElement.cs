using System;
using ItemsManager.Databases.Elements;
using UnityEngine;

namespace ItemsManager.Test.Databases
{
    [Serializable]
    public class TestListAbstractDatabaseElement : IAbstractDatabaseElement
    {
        [field: SerializeField] public string Name { get; private set; }
        
        public int A;
        public int B;
        public Sprite Icon;

        public TestListAbstractDatabaseElement(string name)
        {
            Name = name;
        }
    }
}