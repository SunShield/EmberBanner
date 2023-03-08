using UnityEngine;

namespace EmberBanner.Core.Service.Debug
{
    [CreateAssetMenu(fileName = "EB Debug Parameters", menuName = "EB Debug Parameters")]
    public class EBDebugParameters : ScriptableObject
    {
        public bool debugEnabled;
        public EBDebugContextToColorDictionary contextColors;
    }
}