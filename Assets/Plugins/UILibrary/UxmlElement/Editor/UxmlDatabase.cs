using UILibrary.UxmlElement.Unity.Service;
using UnityEditor;
using UnityEngine;

namespace UILibrary.UxmlElement.Editor
{
    [CreateAssetMenu(menuName = "Service/Uxml Database", fileName = "UxmlDatabase")]
    [FilePath(@"Assets/Data/UxmlDatabase.asset", FilePathAttribute.Location.ProjectFolder)]
    public class UxmlDatabase : ScriptableObject
    {
        private static UxmlDatabase _instance;
        public static UxmlDatabase Instance
        {
            get
            {
                if (_instance == null)
                    _instance = AssetDatabase.LoadAssetAtPath<UxmlDatabase>(@"Assets/Data/UxmlDatabase.asset");
                return _instance;
            }
        }
        
        public StringToVisualTreeAssetDictionary Uxmls;
    }
}