using TMPro;
using UnityEngine;

public class LevelProgessTextRefresher : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI progessText;

    private void OnEnable()
    {
        progessText.text = "Repaired: " + (GameProgress.GetHighestUnlockedLevel() - 1) + " / 6";
    }
}
