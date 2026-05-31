using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompletionController : MonoBehaviour
{
    [Header("Current Level")]
    [SerializeField] private int currentLevelOrder = 1;

    [Header("Navigation")]
    [SerializeField] private string mainMenuSceneName = "Main Menu";
    [SerializeField] private string nextLevelSceneName = "";

    public void CompleteLevel()
    {
        GameProgress.MarkLevelCompleted(currentLevelOrder);
    }

    public void CompleteAndReturnToMenu()
    {
        CompleteLevel();
        SceneManager.LoadScene(mainMenuSceneName);
    }

    public void CompleteAndLoadNextLevel()
    {
        CompleteLevel();

        if (!string.IsNullOrEmpty(nextLevelSceneName))
        {
            SceneManager.LoadScene(nextLevelSceneName);
        }
        else
        {
            SceneManager.LoadScene(mainMenuSceneName);
        }
    }
}