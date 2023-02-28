using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CodeBase.Infrastructure.SceneLoader
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private Button nextScene;

        private void Start()
        {
            nextScene.onClick.AddListener(LoadNextScene);
        }

        private void LoadNextScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}