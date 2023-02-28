using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CodeBase.Infrastructure.SceneLoader
{
    public class SceneLoader : MonoBehaviour
    {
        private const string SceneName = "Gameplay";
        [SerializeField] private Button nextScene;
        [SerializeField] private Button quitGame;

        private void Start()
        {
            nextScene.onClick.AddListener(LoadNextScene);
            quitGame.onClick.AddListener(QuitGame);
        }

        private void LoadNextScene() => 
            SceneManager.LoadScene(SceneName);

        private void QuitGame() => 
            Application.Quit();
    }
}