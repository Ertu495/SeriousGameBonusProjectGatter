using UnityEngine;
using TMPro;

public class Level : MonoBehaviour
{
    public int levelNumber;
    public bool isTutorialLevel;
    public GameObject[] AvailableGates; 

    public string tutorialText;
    public string tutorialTextHeader;

    public string endLevelText;
    public string endLevelHeader;

    public int targetValue;
    public int currentValue;

    public GameObject tutorialTextObject; 
    public GameObject endLevelObject; 
    public GameObject LevelManagerObject;
    public MenuScreenController menuScreenController;


    public void StartLevel()
    {
        menuScreenController = FindFirstObjectByType<MenuScreenController>();
        if (isTutorialLevel && tutorialTextObject != null)
        {
            tutorialTextObject.SetActive(true);
            tutorialTextObject.GetComponent<TutorialText>().SetTutorialText(tutorialText, tutorialTextHeader);
        }
    }

    public void EndLevel()
    {

        if (endLevelObject != null)
        {
            menuScreenController = FindFirstObjectByType<MenuScreenController>();
            menuScreenController.ShowEndLevelUI();

            endLevelObject.GetComponent<TutorialText>().SetTutorialText(endLevelText, endLevelHeader);
        }
        GameProgress.MarkLevelCompleted(levelNumber);
        Debug.Log(GameProgress.GetHighestUnlockedLevel());

        LevelManagerObject.GetComponent<LevelManager>().DestroyLevel();
    }
}