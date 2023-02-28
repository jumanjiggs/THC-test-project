using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase
{
    public class UiManager : MonoBehaviour
    {
        [SerializeField] private GameObject nextButton;

        private void LoadMenuScene()
        {
            SceneManager.LoadScene("Menu");
        }
    }
}