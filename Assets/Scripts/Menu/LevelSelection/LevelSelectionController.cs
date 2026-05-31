using UnityEngine;

public class LevelSelectionController : MonoBehaviour
{
    [SerializeField] private LevelButtonUI[] levelButtons;

    private void OnEnable()
    {
        Refresh();
    }

    public void Refresh()
    {
        foreach (LevelButtonUI levelButton in levelButtons)
        {
            if (levelButton != null)
            {
                levelButton.Refresh();
            }
        }
    }
}