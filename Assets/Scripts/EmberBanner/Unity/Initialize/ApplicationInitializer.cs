using EmberBanner.Core.Entities.Management.Databases;
using EmberBanner.Unity.Constants;
using EmberBanner.Unity.Service;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace EmberBanner.Unity.Initialize
{
    public class ApplicationInitializer : EBMonoBehaviour
    {
        public void Start()
        {
            GeneralEntityDatabase.I.Load();
            TestFillDatabases();
            SceneManager.LoadScene(UnityConstants.Scenes.GameSceneName);
        }

        private void TestFillDatabases()
        {
            if (PlayerPrefs.HasKey("Test")) return;
            
            
            
            PlayerPrefs.SetString("Test", "+");
        }
    }
}