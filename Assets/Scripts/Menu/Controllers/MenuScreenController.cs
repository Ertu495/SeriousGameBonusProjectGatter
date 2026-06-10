using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuScreenController : MonoBehaviour
{
    private const string GameStartedKey = "BooleanMechanic_GameStarted";

    [Header("Menu Screens")]
    [SerializeField] private GameObject mainMenuUI;
    [SerializeField] private GameObject playMenuUI;
    [SerializeField] private GameObject selectLevelMenuUI;
    [SerializeField] private GameObject settingsMenuUI;
    [SerializeField] private GameObject creditsMenuUI;

    [SerializeField] private GameObject levelUI;
    [SerializeField] private GameObject levelTutorialUI;
    [SerializeField] private GameObject endLevelUI;

    [Header("Shared UI")]
    [SerializeField] private GameObject backButtonUI;

    [Header("Main Menu")]
    [SerializeField] private TMP_Text playButtonText;

    [Header("Level Selection")]
    [SerializeField] private LevelSelectionController levelSelectionController;

    [Header("Game Intro Popup")]
    [SerializeField] private GameObject gameIntroPopupOverlay;
    [SerializeField] private TMP_Text gameIntroTitleText;
    [SerializeField] private TMP_Text gameIntroBodyText;
    [SerializeField] private TMP_Text gameIntroButtonText;

    [Header("Scenes / Level Loading")]
    [SerializeField] private int firstLevelIndex = 0;
    [SerializeField] private GameObject levelManager;

    private bool introPopupOpen;

    private void Start()
    {
        CloseGameIntroPopup();
        OpenMainMenu();
        RefreshMainButtonText();
    }

    public void OpenMainMenu()
    {
        ShowOnly(mainMenuUI);
        SetBackButton(false);
        CloseGameIntroPopup();
        RefreshMainButtonText();
    }

    public void OpenPlayMenu()
    {
        ShowOnly(playMenuUI);
        SetBackButton(true);
        RefreshLevelSelection();
    }

    public void OpenSelectLevelMenu()
    {
        ShowOnly(selectLevelMenuUI);
        SetBackButton(true);
        RefreshLevelSelection();
    }

    public void ShowLevelUI()
    {
        ShowOnly(levelUI);
        SetBackButton(true);
    }

    public void ShowEndLevelUI()
    {
        ShowOnly(endLevelUI);
        SetBackButton(true);
    }

    public void ShowLevelTutorialUI()
    {
        ShowOnly(levelTutorialUI);
        SetBackButton(true);
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
        bool gameAlreadyStarted = PlayerPrefs.GetInt(GameStartedKey, 0) == 1;

        if (gameAlreadyStarted)
        {
            OpenSelectLevelMenu();
            return;
        }

        OpenSelectLevelMenu();
        OpenGameIntroPopup();
    }

    public void StartNewGame()
    {
        OnPlayClicked();
    }

    public void ContinueIntroToTutorial1()
    {
        PlayerPrefs.SetInt(GameStartedKey, 1);
        PlayerPrefs.Save();

        CloseGameIntroPopup();
        RefreshMainButtonText();
        OpenSelectLevelMenu();

        // Keep this commented if you want the player to choose Level 1 manually.
        // LoadLevel(firstLevelIndex);
    }

    public void OpenGameIntroPopup()
    {
        if (gameIntroPopupOverlay == null)
        {
            Debug.LogError("GameIntroPopupOverlay is not assigned.");
            return;
        }

        introPopupOpen = true;

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
        gameIntroPopupOverlay.transform.SetAsLastSibling();
    }

    public void CloseGameIntroPopup()
    {
        introPopupOpen = false;

        if (gameIntroPopupOverlay != null)
        {
            gameIntroPopupOverlay.SetActive(false);
        }
    }

    public void LoadLevel(int levelIndex)
    {
        if (levelManager == null)
        {
            Debug.LogError("LevelManager is not assigned in MenuScreenController.");
            return;
        }

        LevelManager manager = levelManager.GetComponent<LevelManager>();

        if (manager == null)
        {
            Debug.LogError("Assigned LevelManager object does not have LevelManager script.");
            return;
        }

        ShowOnly(null);
        SetBackButton(false);
        CloseGameIntroPopup();

        manager.CreateLevel(levelIndex);
    }

    public void ResetProgress()
    {
        GameProgress.ResetProgress();
        PlayerPrefs.DeleteKey(GameStartedKey);
        PlayerPrefs.Save();

        RefreshLevelSelection();
        RefreshMainButtonText();
        OpenMainMenu();
    }

    public void ResetProgressForTesting()
    {
        ResetProgress();
    }

    public void ExitGame()
    {
        Debug.Log("Exit game requested.");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    private void RefreshMainButtonText()
    {
        if (playButtonText == null)
            return;

        bool gameAlreadyStarted = PlayerPrefs.GetInt(GameStartedKey, 0) == 1;
        playButtonText.text = gameAlreadyStarted ? "Continue" : "Start Game";
    }

    private void RefreshLevelSelection()
    {
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
        SetActive(levelUI, levelUI == targetMenu);
        SetActive(levelTutorialUI, levelTutorialUI == targetMenu);
        SetActive(endLevelUI, endLevelUI == targetMenu);
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