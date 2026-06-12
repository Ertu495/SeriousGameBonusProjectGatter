using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelPopupController : MonoBehaviour
{
    [Header("Core References")]
    [SerializeField] private MenuScreenController menuScreenController;
    [SerializeField] private LevelManager levelManager;

    [Header("Level Frame UI")]
    [SerializeField] private GameObject levelFramePanel;
    [SerializeField] private Button toLevelMenuButton;
    [SerializeField] private Button tutorialPopupButton;

    [Header("Tutorial Popup")]
    [SerializeField] private GameObject tutorialPopupOverlay;
    [SerializeField] private TMP_Text tutorialTitleText;
    [SerializeField] private TMP_Text tutorialBodyText;
    [SerializeField] private TMP_Text tutorialTruthTableText;
    [SerializeField] private Button tutorialHideButton;
    [SerializeField] private Button tutorialContinueButton;

    [Header("Success Popup")]
    [SerializeField] private GameObject successPopupOverlay;
    [SerializeField] private TMP_Text successTitleText;
    [SerializeField] private TMP_Text successBodyText;
    [SerializeField] private TMP_Text successTruthTableText;
    [SerializeField] private Button successContinueButton;

    [Header("End Game Popup")]
    [SerializeField] private GameObject endGamePopupOverlay;
    [SerializeField] private TMP_Text endGameTitleText;
    [SerializeField] private TMP_Text endGameBodyText;
    [SerializeField] private Button endGameContinueButton;

    private int currentLevelNumber;
    private bool successAlreadyShown;


    private void Awake()
    {
        ConnectButtons();
        HideLevelRuntimeUI();
    }

    private void ConnectButtons()
    {
        if (toLevelMenuButton != null)
            toLevelMenuButton.onClick.AddListener(ReturnToLevelSelection);

        if (tutorialPopupButton != null)
            tutorialPopupButton.onClick.AddListener(ShowTutorialForCurrentLevel);

        if (tutorialHideButton != null)
            tutorialHideButton.onClick.AddListener(HideTutorialPopup);

        if (tutorialContinueButton != null)
            tutorialContinueButton.onClick.AddListener(HideTutorialPopup);

        if (successContinueButton != null)
            successContinueButton.onClick.AddListener(ContinueAfterSuccess);

        if (endGameContinueButton != null)
            //endGameContinueButton.onClick.AddListener(ReturnToLevelSelection);
            endGameContinueButton.onClick.AddListener(rollCredits);
            
    }

    public void rollCredits()
{
    HideLevelRuntimeUI();

    if (levelManager != null)
    {
        levelManager.DestroyLevel();
    }

    if (menuScreenController != null)
    {
        menuScreenController.OpenCredits();
    }
}

    public void BeginLevel(int levelNumber)
    {
        currentLevelNumber = levelNumber;
        successAlreadyShown = false;

        HideAllPopups();

        if (levelFramePanel != null)
            levelFramePanel.SetActive(true);

        bool hasTutorial = HasTutorial(levelNumber);

        if (tutorialPopupButton != null)
        {
            tutorialPopupButton.gameObject.SetActive(hasTutorial);
            tutorialPopupButton.interactable = hasTutorial;
        }

        if (hasTutorial)
        {
            ShowTutorialPopup(levelNumber);
        }
    }

    public void ShowTutorialForCurrentLevel()
    {
        if (!HasTutorial(currentLevelNumber))
            return;

        ShowTutorialPopup(currentLevelNumber);
    }

    private void ShowTutorialPopup(int levelNumber)
    {
        PopupContent content = GetTutorialContent(levelNumber);

        SetText(tutorialTitleText, content.title);
        SetText(tutorialBodyText, content.body);
        SetText(tutorialTruthTableText, content.truthTable);

        ShowOverlay(tutorialPopupOverlay);
    }

    public void HideTutorialPopup()
    {
        HideOverlay(tutorialPopupOverlay);
    }

    public void ShowSuccessForLevel(int levelNumber)
    {
        if (successAlreadyShown)
            return;

        successAlreadyShown = true;
        currentLevelNumber = levelNumber;

        GameProgress.MarkLevelCompleted(levelNumber);

        PopupContent content = GetSuccessContent(levelNumber);

        SetText(successTitleText, content.title);
        SetText(successBodyText, content.body);
        SetText(successTruthTableText, content.truthTable);

        HideOverlay(tutorialPopupOverlay);
        ShowOverlay(successPopupOverlay);
    }

    private void ContinueAfterSuccess()
    {
        HideOverlay(successPopupOverlay);

        if (currentLevelNumber >= 6)
        {
            ShowEndGamePopup();
            return;
        }

        ReturnToLevelSelection();
    }

    private void ShowEndGamePopup()
    {
        if (levelFramePanel != null)
            levelFramePanel.SetActive(false);

        SetText(endGameTitleText, "Spaceship Repaired");

        SetText(
            endGameBodyText,
            "You won the game.\n\n" +
            "All 6 spaceship components have been repaired. The Boolean circuits are stable, " +
            "the damaged cables are connected, and the ship can continue its mission.\n\n" +
            "Final progress: 6/6 components repaired."
        );

        ShowOverlay(endGamePopupOverlay);
    }

    public void ReturnToLevelSelection()
    {
        HideLevelRuntimeUI();

        if (levelManager != null)
            levelManager.DestroyLevel();

        if (menuScreenController != null)
            menuScreenController.OpenSelectLevelMenu();
    }

    private void HideLevelRuntimeUI()
    {
        if (levelFramePanel != null)
            levelFramePanel.SetActive(false);

        HideAllPopups();
    }

    private void HideAllPopups()
    {
        HideOverlay(tutorialPopupOverlay);
        HideOverlay(successPopupOverlay);
        HideOverlay(endGamePopupOverlay);
    }

    private bool HasTutorial(int levelNumber)
    {
        return levelNumber >= 1 && levelNumber <= 3;
    }

    private void ShowOverlay(GameObject overlay)
    {
        if (overlay == null)
            return;

        overlay.SetActive(true);
        overlay.transform.SetAsLastSibling();
    }

    private void HideOverlay(GameObject overlay)
    {
        if (overlay != null)
            overlay.SetActive(false);
    }

    private void SetText(TMP_Text target, string value)
    {
        if (target != null)
            target.text = value;
    }

    private PopupContent GetTutorialContent(int level)
    {
        switch (level)
        {
            case 1:
                return new PopupContent
                {
                    title = "Tutorial Level 1 — NOT Gate",
                    body =
                        "Repair task: restore the warning inverter cable.\n\n" +
                        "The NOT gate reverses a signal.\n\n" +
                        "If the input is 0, the output becomes 1. If the input is 1, the output becomes 0.\n\n" +
                        "A = System safe\n" +
                        "OUT = Warning active\n\n" +
                        "Use NOT when the circuit must produce the opposite signal.",
                    truthTable =
                        "<mspace=0.65em>" +
                        "A | OUT\n" +
                        "--+----\n" +
                        "0 |  1\n" +
                        "1 |  0" +
                        "</mspace>"
                };

            case 2:
                return new PopupContent
                {
                    title = "Tutorial Level 2 — OR Gate",
                    body =
                        "Repair task: restore the emergency power cable.\n\n" +
                        "The OR gate outputs 1 when at least one input is 1.\n\n" +
                        "A = Main generator active\n" +
                        "B = Backup battery active\n" +
                        "OUT = Power available\n\n" +
                        "Use OR when one working source is enough to activate the system.",
                    truthTable =
                        "<mspace=0.65em>" +
                        "A | B | OUT\n" +
                        "--+---+----\n" +
                        "0 | 0 |  0\n" +
                        "0 | 1 |  1\n" +
                        "1 | 0 |  1\n" +
                        "1 | 1 |  1" +
                        "</mspace>"
                };

            default:
                return new PopupContent
                {
                    title = "Tutorial Level 3 — AND Gate",
                    body =
                        "Repair task: restore the airlock safety cable.\n\n" +
                        "The AND gate outputs 1 only when both inputs are 1.\n\n" +
                        "A = Crew ID verified\n" +
                        "B = Air pressure stable\n" +
                        "OUT = Airlock unlock signal\n\n" +
                        "Use AND when two conditions must be true at the same time.",
                    truthTable =
                        "<mspace=0.65em>" +
                        "A | B | OUT\n" +
                        "--+---+----\n" +
                        "0 | 0 |  0\n" +
                        "0 | 1 |  0\n" +
                        "1 | 0 |  0\n" +
                        "1 | 1 |  1" +
                        "</mspace>"
                };
        }
    }

    private PopupContent GetSuccessContent(int level)
    {
        switch (level)
        {
            case 1:
                return new PopupContent
                {
                    title = "Level 1 Completed — Warning Cable Repaired",
                    body =
                        "Correct solution.\n\n" +
                        "The warning signal must be active when the system is not safe. " +
                        "That is why the NOT gate is the correct repair component.\n\n" +
                        "Progress: 1/6 components repaired.",
                    truthTable =
                        "<mspace=0.65em>" +
                        "A | OUT\n" +
                        "--+----\n" +
                        "0 |  1\n" +
                        "1 |  0" +
                        "</mspace>"
                };

            case 2:
                return new PopupContent
                {
                    title = "Level 2 Completed — Emergency Power Repaired",
                    body =
                        "Correct solution.\n\n" +
                        "The ship needs power if either the main generator or the backup battery is active. " +
                        "That is why the OR gate is the correct repair component.\n\n" +
                        "Progress: 2/6 components repaired.",
                    truthTable =
                        "<mspace=0.65em>" +
                        "A | B | OUT\n" +
                        "--+---+----\n" +
                        "0 | 0 |  0\n" +
                        "0 | 1 |  1\n" +
                        "1 | 0 |  1\n" +
                        "1 | 1 |  1" +
                        "</mspace>"
                };

            case 3:
                return new PopupContent
                {
                    title = "Level 3 Completed — Airlock Safety Repaired",
                    body =
                        "Correct solution.\n\n" +
                        "The airlock may unlock only when the crew ID is verified and the air pressure is stable. " +
                        "That is why the AND gate is the correct repair component.\n\n" +
                        "Progress: 3/6 components repaired.",
                    truthTable =
                        "<mspace=0.65em>" +
                        "A | B | OUT\n" +
                        "--+---+----\n" +
                        "0 | 0 |  0\n" +
                        "0 | 1 |  0\n" +
                        "1 | 0 |  0\n" +
                        "1 | 1 |  1" +
                        "</mspace>"
                };

            case 4:
                return new PopupContent
                {
                    title = "Level 4 Completed — Cooling Safety Cable Repaired",
                    body =
                        "Correct solution.\n\n" +
                        "The cooling system works only when the activation signal is active and the danger signal is not active.\n\n" +
                        "Formula: OUT = A AND NOT B\n\n" +
                        "Progress: 4/6 components repaired.",
                    truthTable =
                        "<mspace=0.65em>" +
                        "A | B | OUT\n" +
                        "--+---+----\n" +
                        "0 | 0 |  0\n" +
                        "0 | 1 |  0\n" +
                        "1 | 0 |  1\n" +
                        "1 | 1 |  0" +
                        "</mspace>"
                };

            case 5:
                return new PopupContent
                {
                    title = "Level 5 Completed — Backup Alarm Repaired",
                    body =
                        "Correct solution.\n\n" +
                        "The backup alarm activates if the manual alarm is active or if the automatic safety check is not passed.\n\n" +
                        "Formula: OUT = A OR NOT B\n\n" +
                        "Progress: 5/6 components repaired.",
                    truthTable =
                        "<mspace=0.65em>" +
                        "A | B | OUT\n" +
                        "--+---+----\n" +
                        "0 | 0 |  1\n" +
                        "0 | 1 |  0\n" +
                        "1 | 0 |  1\n" +
                        "1 | 1 |  1" +
                        "</mspace>"
                };

            default:
                return new PopupContent
                {
                    title = "Level 6 Completed — Main Control Circuit Repaired",
                    body =
                        "Correct solution.\n\n" +
                        "The final control system works only when the master repair signal is active and at least one navigation source is available.\n\n" +
                        "Formula: OUT = A AND (B OR C)\n\n" +
                        "Progress: 6/6 components repaired.",
                    truthTable =
                        "<mspace=0.65em>" +
                        "A | B | C | OUT\n" +
                        "--+---+---+----\n" +
                        "0 | 0 | 0 |  0\n" +
                        "0 | 0 | 1 |  0\n" +
                        "0 | 1 | 0 |  0\n" +
                        "0 | 1 | 1 |  0\n" +
                        "1 | 0 | 0 |  0\n" +
                        "1 | 0 | 1 |  1\n" +
                        "1 | 1 | 0 |  1\n" +
                        "1 | 1 | 1 |  1" +
                        "</mspace>"
                };
        }
    }

    private struct PopupContent
    {
        public string title;
        public string body;
        public string truthTable;
    }
}