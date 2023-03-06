using EmberBanner.Unity.Constants;
using EmberBanner.Unity.Service;
using UnityEngine.SceneManagement;

namespace EmberBanner.Unity.Game.Startup
{
    public class GameStarter : EBMonoBehaviour
    {
        public void Start()
        {
            SceneManager.LoadScene(UnityConstants.Scenes.BattleSceneName);
        }
    }
}