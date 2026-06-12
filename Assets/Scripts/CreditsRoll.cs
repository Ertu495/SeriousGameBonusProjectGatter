using UnityEngine;

public class CreditsRoll : MonoBehaviour
{
    public float scrollSpeed = 125f; 
    public float stopPositionY = 1050f; 

    public MenuScreenController menuController; 

    private RectTransform rectTransform;
    private Vector2 startPosition;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        startPosition = rectTransform.anchoredPosition;
    }

    void OnEnable()
    {
        if (rectTransform != null)
        {
            rectTransform.anchoredPosition = startPosition;
        }
    }

    void Update()
    {
        if (rectTransform.anchoredPosition.y < stopPositionY)
        {
            transform.Translate(Vector3.up * scrollSpeed * Time.deltaTime);
        }
        else
        {
            if (menuController != null)
            {
                menuController.OpenMainMenu();
                rectTransform.anchoredPosition = startPosition; 
            }
        }
    }
}