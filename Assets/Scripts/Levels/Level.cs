using UnityEngine;

public class Level : MonoBehaviour
{
    [Header("Level Settings")]
    public int levelNumber;
    public bool isTutorialLevel;
    public GameObject[] AvailableGates;

    [Header("Old Text Fields - Not Used By New Popup System")]
    public string tutorialText;
    public string tutorialTextHeader;
    public string endLevelText;
    public string endLevelHeader;

    [Header("Level Values")]
    public int targetValue;
    public int currentValue;

    [Header("Old UI References - Not Used By New Popup System")]
    public GameObject tutorialTextObject;
    public GameObject endLevelObject;
    public GameObject LevelManagerObject;
    public MenuScreenController menuScreenController;

    public void StartLevel()
    {
        // New popup system is started by MenuScreenController + LevelPopupController.
        // This method remains because LevelManager calls it after creating a level.
    }

    public void EndLevel()
    {
        LevelPopupController popupController = FindFirstObjectByType<LevelPopupController>();

        if (popupController == null)
        {
            Debug.LogError("No LevelPopupController found in Main scene. Cannot show success popup.");
            return;
        }

        popupController.ShowSuccessForLevel(levelNumber);
    }
}