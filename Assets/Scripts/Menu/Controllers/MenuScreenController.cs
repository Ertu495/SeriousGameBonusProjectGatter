using TMPro;
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

    [Header("Game Intro Popup")]
    [SerializeField] private GameObject gameIntroPopupOverlay;
    [SerializeField] private TMP_Text gameIntroTitleText;
    [SerializeField] private TMP_Text gameIntroBodyText;
    [SerializeField] private TMP_Text gameIntroButtonText;

    [Header("Scenes")]
    [SerializeField] private string firstTutorialSceneName = "Tutorial1";

    private void Start()
    {
        CloseGameIntroPopup();

        if (MenuNavigationState.OpenLevelSelectionOnStart)
        {
            MenuNavigationState.OpenLevelSelectionOnStart = false;
            OpenSelectLevelMenu();
        }
        else
        {
            OpenMainMenu();
        }
    }

    public void OpenMainMenu()
    {
        ShowOnly(mainMenuUI);
        SetBackButton(false);
        CloseGameIntroPopup();
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
        CloseGameIntroPopup();
    }

    public void OpenCredits()
    {
        ShowOnly(creditsMenuUI);
        SetBackButton(true);
        CloseGameIntroPopup();
    }

    public void OnPlayClicked()
    {
        OpenSelectLevelMenu();

        if (!GameSessionState.GameIntroWasShown)
        {
            OpenGameIntroPopup();
        }
    }

    public void StartNewGame()
    {
        OnPlayClicked();
    }

    public void ContinueIntroToTutorial1()
    {
        GameSessionState.GameIntroWasShown = true;
        CloseGameIntroPopup();
        LoadLevel(firstTutorialSceneName);
    }

    public void OpenGameIntroPopup()
    {
        if (gameIntroPopupOverlay == null)
        {
            Debug.LogError("GameIntroPopupOverlay is not assigned.");
            return;
        }

        if (gameIntroTitleText != null)
        {
            gameIntroTitleText.text = "Welcome to Boolean Mechanic!";
        }

        if (gameIntroBodyText != null)
        {
            gameIntroBodyText.text =
                "Your spaceship has crash-landed on a mysterious planet far from Earth.\n\n" +
                "The ship's control systems were heavily damaged, and many of its logic circuits are no longer functioning correctly.\n\n" +
                "To repair the ship and make your way home, you must solve circuit puzzles using Boolean logic gates.";
        }

        if (gameIntroButtonText != null)
        {
            gameIntroButtonText.text = "Continue";
        }

        gameIntroPopupOverlay.SetActive(true);
    }

    public void CloseGameIntroPopup()
    {
        if (gameIntroPopupOverlay != null)
        {
            gameIntroPopupOverlay.SetActive(false);
        }
    }

    public void LoadLevel(string sceneName)
    {
        if (string.IsNullOrWhiteSpace(sceneName))
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

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
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