using UnityEngine;

public static class GameProgress
{
    private const string HighestUnlockedLevelKey = "HighestUnlockedLevelOrder";
    private const int FirstUnlockedLevel = 1;

    public static int GetHighestUnlockedLevel()
    {
        return PlayerPrefs.GetInt(HighestUnlockedLevelKey, FirstUnlockedLevel);
    }

    public static bool IsLevelUnlocked(int levelOrder)
    {
        return levelOrder <= GetHighestUnlockedLevel();
    }

    public static void MarkLevelCompleted(int completedLevelOrder)
    {
        int nextLevelOrder = completedLevelOrder + 1;

        if (nextLevelOrder > GetHighestUnlockedLevel())
        {
            PlayerPrefs.SetInt(HighestUnlockedLevelKey, nextLevelOrder);
            PlayerPrefs.Save();
        }
    }

    public static void ResetProgress()
    {
        PlayerPrefs.SetInt(HighestUnlockedLevelKey, FirstUnlockedLevel);
        PlayerPrefs.Save();
    }
}