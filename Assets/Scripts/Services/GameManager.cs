
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Services
{
    public class GameManager : ServiceBase
    {
        public void StartGame()
        {
            SceneManager.LoadScene(1);
        }

        public void ReplayLevel()
        {
            SceneManager.LoadScene(1);
        }
        public void NextLevel()
        {
            SceneManager.LoadScene(1);
        }
    }
}