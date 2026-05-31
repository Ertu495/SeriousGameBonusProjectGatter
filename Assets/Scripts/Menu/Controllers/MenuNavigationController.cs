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

    public void LoadLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
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
    }
}