using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CodeBase.Infrastructure.SceneLoader
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private Button nextScene;
        [SerializeField] private Button quitGame;

        private void Start()
        {
            nextScene.onClick.AddListener(LoadNextScene);
            quitGame.onClick.AddListener(QuitGame);
        }

        private void LoadNextScene() => 
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        private void QuitGame() => 
            Application.Quit();
    }
}