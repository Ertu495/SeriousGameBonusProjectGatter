using UnityEngine;
using TMPro;

public class TutorialText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tutorialTextHeaderUI;
    [SerializeField] private TextMeshProUGUI tutorialTextUI;

    public void SetTutorialText(string text, string header)
    {
        if (tutorialTextUI != null)
        {
            tutorialTextUI.SetText(text);
        }
        if (tutorialTextHeaderUI != null)
        {
            tutorialTextHeaderUI.SetText(header);
        }
    }
}
