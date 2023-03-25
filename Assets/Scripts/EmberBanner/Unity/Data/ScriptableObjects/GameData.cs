using EmberBanner.Core.Service.Classes.Collections;
using UnityEngine;

namespace EmberBanner.Unity.Data.ScriptableObjects
{
    [CreateAssetMenu(menuName = "EB/Game Data", fileName = "GameData")]
    public class GameData : ScriptableObject
    {
#if UNITY_EDITOR

        private static GameData _editorInstance;

        public static GameData EI
        {
            get
            {
                if (_editorInstance == null)
                    _editorInstance = UnityEditor.AssetDatabase.LoadAssetAtPath<GameData>(@"Assets/Data/GameData.asset");
                return _editorInstance;
            }
        }
        
#endif
        
        public StringToSpriteDictionary ActionTypeIcons;
        public ActionTypeToColorDictionary ActionTypeColors;
    }
}