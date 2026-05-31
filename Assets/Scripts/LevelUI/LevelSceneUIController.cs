using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class LevelPopupPage
{
    public string title;

    [TextArea(3, 10)]
    public string body;

    public string buttonText = "Continue";
}

public class LevelSceneUIController : MonoBehaviour
{
    private const string MainMenuSceneName = "Main Menu";

    [Header("Tutorial Popup")]
    [SerializeField] private GameObject tutorialPopupOverlay;
    [SerializeField] private TMP_Text tutorialTitleText;
    [SerializeField] private TMP_Text tutorialBodyText;
    [SerializeField] private TMP_Text tutorialButtonText;
    [SerializeField] private LevelPopupPage[] tutorialPages;
    [SerializeField] private bool showTutorialOnSceneStart = false;

    [Header("Success Popup")]
    [SerializeField] private GameObject successPopupOverlay;
    [SerializeField] private string nextSceneName;

    private int currentTutorialPage;

    private void Start()
    {
        if (tutorialPopupOverlay != null)
        {
            tutorialPopupOverlay.SetActive(false);
        }

        if (successPopupOverlay != null)
        {
            successPopupOverlay.SetActive(false);
        }

        if (showTutorialOnSceneStart && tutorialPages != null && tutorialPages.Length > 0)
        {
            OpenTutorialPopup();
        }
    }

    public void BackToLevelSelection()
    {
        MenuNavigationState.OpenLevelSelectionOnStart = true;
        SceneManager.LoadScene(MainMenuSceneName);
    }

    public void OpenTutorialPopup()
    {
        if (tutorialPopupOverlay == null || tutorialPages == null || tutorialPages.Length == 0)
        {
            return;
        }

        currentTutorialPage = 0;
        ShowTutorialPage();
        tutorialPopupOverlay.SetActive(true);
    }

    public void TutorialNextOrClose()
    {
        if (tutorialPages == null || tutorialPages.Length == 0)
        {
            CloseTutorialPopup();
            return;
        }

        if (currentTutorialPage < tutorialPages.Length - 1)
        {
            currentTutorialPage++;
            ShowTutorialPage();
        }
        else
        {
            CloseTutorialPopup();
        }
    }

    public void CloseTutorialPopup()
    {
        if (tutorialPopupOverlay != null)
        {
            tutorialPopupOverlay.SetActive(false);
        }
    }

    public void ShowSuccessPopup()
    {
        if (successPopupOverlay != null)
        {
            successPopupOverlay.SetActive(true);
        }
    }

    public void CloseSuccessPopup()
    {
        if (successPopupOverlay != null)
        {
            successPopupOverlay.SetActive(false);
        }
    }

    public void NextAfterSuccess()
    {
        if (!string.IsNullOrWhiteSpace(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            BackToLevelSelection();
        }
    }

    private void ShowTutorialPage()
    {
        LevelPopupPage page = tutorialPages[currentTutorialPage];

        if (tutorialTitleText != null)
        {
            tutorialTitleText.text = page.title;
        }

        if (tutorialBodyText != null)
        {
            tutorialBodyText.text = page.body;
        }

        if (tutorialButtonText != null)
        {
            tutorialButtonText.text = page.buttonText;
        }
    }
}