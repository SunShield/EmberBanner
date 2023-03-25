using UnityEngine;

namespace EmberBanner.Core.Service.Debug
{
    [CreateAssetMenu( menuName = "EB/EB Debug Parameters", fileName = "EB Debug Parameters")]
    public class EBDebugParameters : ScriptableObject
    {
        public bool debugEnabled;
        public EBDebugContextToColorDictionary contextColors;
    }
}