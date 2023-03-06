using ItemsManager.Databases;
using UnityEngine;

namespace ItemsManager.Test.Databases
{
    [CreateAssetMenu(menuName = "ItemsManager/Create Test General Db", fileName = "TestGeneralDb")]
    public class TestGeneralDatabase : AbstractGeneralDatabase
    {
        public TestListDatabase ListDb;
    }
}