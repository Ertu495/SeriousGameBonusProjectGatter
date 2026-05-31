using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScreenController : MonoBehaviour
{
    [Header("Menu Screens")]
    [SerializeField] private GameObject mainMenuUI;
    [SerializeField] private GameObject playMenuUI;
    [SerializeField] private GameObject selectLevelMenuUI;
    [SerializeField] private GameObject settingsMenuUI;
    [SerializeField] private GameObject creditsMenuUI;

    [Header("Shared UI")]
    [SerializeField] private GameObject backButtonUI;

    [Header("Level Selection")]
    [SerializeField] private LevelSelectionController levelSelectionController;

    [Header("Scenes")]
    [SerializeField] private string firstLevelSceneName = "SampleScene";

    private void Start()
    {
        OpenMainMenu();
    }

    public void OpenMainMenu()
    {
        ShowOnly(mainMenuUI);
        SetBackButton(false);
    }

    public void OpenPlayMenu()
    {
        ShowOnly(playMenuUI);
        SetBackButton(true);
    }

    public void OpenSelectLevelMenu()
    {
        ShowOnly(selectLevelMenuUI);
        SetBackButton(true);

        if (levelSelectionController != null)
        {
            levelSelectionController.Refresh();
        }
    }

    public void OpenSettings()
    {
        ShowOnly(settingsMenuUI);
        SetBackButton(true);
    }

    public void OpenCredits()
    {
        ShowOnly(creditsMenuUI);
        SetBackButton(true);
    }

    public void StartNewGame()
    {
        LoadLevel(firstLevelSceneName);
    }

    public void LoadLevel(string sceneName)
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("Scene name is empty. Cannot load level.");
            return;
        }

        SceneManager.LoadScene(sceneName);
    }

    public void ExitGame()
    {
        Debug.Log("Exit game requested.");
        Application.Quit();
    }

    public void ResetProgressForTesting()
    {
        GameProgress.ResetProgress();

        if (levelSelectionController != null)
        {
            levelSelectionController.Refresh();
        }
    }

    private void ShowOnly(GameObject targetMenu)
    {
        SetActive(mainMenuUI, mainMenuUI == targetMenu);
        SetActive(playMenuUI, playMenuUI == targetMenu);
        SetActive(selectLevelMenuUI, selectLevelMenuUI == targetMenu);
        SetActive(settingsMenuUI, settingsMenuUI == targetMenu);
        SetActive(creditsMenuUI, creditsMenuUI == targetMenu);
    }

    private void SetBackButton(bool visible)
    {
        SetActive(backButtonUI, visible);
    }

    private static void SetActive(GameObject target, bool active)
    {
        if (target != null)
        {
            target.SetActive(active);
        }
    }
}