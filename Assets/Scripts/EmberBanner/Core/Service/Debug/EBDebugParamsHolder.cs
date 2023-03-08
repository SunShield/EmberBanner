using EmberBanner.Unity.Service;

namespace EmberBanner.Core.Service.Debug
{
    public class EBDebugParamsHolder : EBMonoBehaviour
    {
        private static EBDebugParamsHolder _instance;
        public static EBDebugParamsHolder I
        {
            get
            {
                if (_instance == null)
                    _instance = FindObjectOfType<EBDebugParamsHolder>();
                return _instance;
            }
        }

        private void Awake() => DontDestroyOnLoad(gameObject);

        public EBDebugParameters Parameters;
    }
}