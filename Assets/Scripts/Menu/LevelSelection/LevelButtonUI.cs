using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LevelButtonUI : MonoBehaviour
{
    [Header("Level Data")]
    [SerializeField] private int levelOrder = 1;

    [SerializeField] private bool isImplemented = true;

    [Header("UI References")]
    [SerializeField] private GameObject lockedOverlay;
    [SerializeField] private TMP_Text labelText;
    [SerializeField] private Image buttonImage;

    [Header("Colors")]
    [SerializeField] private Color unlockedColor = Color.white;
    [SerializeField] private Color lockedColor = new Color(0.6f, 0.6f, 0.6f, 1f);

    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        Refresh();
    }

    public void Refresh()
    {
        if (button == null)
        {
            button = GetComponent<Button>();
        }

        bool unlocked = isImplemented && GameProgress.IsLevelUnlocked(levelOrder);

        button.interactable = unlocked;

        if (lockedOverlay != null)
        {
            lockedOverlay.SetActive(!unlocked);
        }

        if (labelText != null)
        {
            labelText.text = levelOrder.ToString();
        }

        if (buttonImage != null)
        {
            buttonImage.color = unlocked ? unlockedColor : lockedColor;
        }
    }

}