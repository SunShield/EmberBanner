using EmberBanner.Unity.Constants;
using EmberBanner.Unity.Service;
using UnityEngine.SceneManagement;

namespace EmberBanner.Unity.Initialize
{
    public class ApplicationInitializer : EBMonoBehaviour
    {
        public void Start()
        {
            SceneManager.LoadScene(UnityConstants.Scenes.GameSceneName);
        }
    }
}