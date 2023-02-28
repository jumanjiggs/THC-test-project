using CodeBase.Hexagons;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CodeBase.Ui
{
    public class UiManager : MonoBehaviour
    {
        private const string SceneName = "Menu";
        
        [SerializeField] private UIPanel winUI;
        [SerializeField] private Button nextButton;
        [SerializeField] private SpawnerHexagons spawnerHexagons;

        private void OnEnable() =>
            spawnerHexagons.allBucketsCompleted.AddListener(ActivateWinUi);

        private void Start() => 
            nextButton.onClick.AddListener(LoadMenuScene);

        private void OnDisable() =>
            spawnerHexagons?.allBucketsCompleted.RemoveListener(ActivateWinUi);

        private void LoadMenuScene() => 
            SceneManager.LoadScene(SceneName);

        private void ActivateWinUi() => 
            winUI.SwitchPanelByParameter(true);
    }
}