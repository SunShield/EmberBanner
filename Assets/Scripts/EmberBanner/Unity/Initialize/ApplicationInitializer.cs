using EmberBanner.Core.Entities.Management.Databases;
using EmberBanner.Unity.Constants;
using EmberBanner.Unity.Service;
using UnityEngine.SceneManagement;

namespace EmberBanner.Unity.Initialize
{
    public class ApplicationInitializer : EBMonoBehaviour
    {
        public void Start()
        {
            GeneralEntityDatabase.I.Load();
            SceneManager.LoadScene(UnityConstants.Scenes.GameSceneName);
        }
    }
}