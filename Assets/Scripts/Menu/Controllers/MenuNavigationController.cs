using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuNavigationController : MonoBehaviour
{
    [Header("Menu Screens")]
    [SerializeField] private GameObject mainMenuUI;
    [SerializeField] private GameObject playMenuUI;
    [SerializeField] private GameObject settingsMenuUI;
    [SerializeField] private GameObject creditsMenuUI;
    [SerializeField] private GameObject levelSelectionUI;
    [SerializeField] private GameObject levelUI;
    [SerializeField] private GameObject levelTutorialUI;
    [SerializeField] private GameObject endLevelUI; 


    private void Start()
    {
        ShowMainMenu();
    }

    public void ShowMainMenu()
    {
        HideAllMenus();
        mainMenuUI.SetActive(true);
    }

    public void ShowPlayMenu()
    {
        HideAllMenus();
        playMenuUI.SetActive(true);
    }

    public void ShowSettingsMenu()
    {
        HideAllMenus();
        settingsMenuUI.SetActive(true);
    }

    public void ShowLevelUI()
    {
        HideAllMenus();
        levelUI.SetActive(true);
    }

    public void ShowLevelTutorialUI()
    {
        HideAllMenus();
        levelTutorialUI.SetActive(true);
    }

    public void ShowCreditsMenu()
    {
        HideAllMenus();
        creditsMenuUI.SetActive(true);
    }

    public void ShowLevelSelection()
    {
        HideAllMenus();
        levelSelectionUI.SetActive(true);
    }

    public void ShowEndLevelUI()
    {
        HideAllMenus();
        endLevelUI.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    private void HideAllMenus()
    {
        mainMenuUI.SetActive(false);

        if (playMenuUI != null)
            playMenuUI.SetActive(false);

        if (settingsMenuUI != null)
            settingsMenuUI.SetActive(false);

        if (creditsMenuUI != null)
            creditsMenuUI.SetActive(false);

        if (levelSelectionUI != null)
            levelSelectionUI.SetActive(false);

        if (levelUI != null)
            levelUI.SetActive(false);

        if (levelTutorialUI != null)
            levelTutorialUI.SetActive(false);

        if (endLevelUI != null)
            endLevelUI.SetActive(false);
    }
}