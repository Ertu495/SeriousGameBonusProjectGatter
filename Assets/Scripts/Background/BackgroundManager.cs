using UnityEngine;

public class BackgroundObjectManager : MonoBehaviour
{
    [Header("Background Objects")]
    [SerializeField] private GameObject[] backgrounds;

    [Header("Start Settings")]
    [SerializeField] private int startBackgroundIndex = 0;

    [Header("Scaling")]
    [SerializeField] private bool fitActiveBackgroundToCamera = true;

    private int currentIndex = -1;

    private void Awake()
    {
        // Optional: Wenn du das Array leer lässt,
        // sammelt der Manager automatisch alle Child-Objekte ein.
        if (backgrounds == null || backgrounds.Length == 0)
        {
            backgrounds = new GameObject[transform.childCount];

            for (int i = 0; i < transform.childCount; i++)
            {
                backgrounds[i] = transform.GetChild(i).gameObject;
            }
        }

        SetBackground(startBackgroundIndex);
    }

    public void SetBackground(int index)
    {
        if (backgrounds == null || backgrounds.Length == 0)
        {
            Debug.LogWarning("Keine Backgrounds gesetzt.");
            return;
        }

        if (index < 0 || index >= backgrounds.Length)
        {
            Debug.LogWarning("Ungültiger Background-Index: " + index);
            return;
        }

        for (int i = 0; i < backgrounds.Length; i++)
        {
            if (backgrounds[i] != null)
            {
                backgrounds[i].SetActive(i == index);
            }
        }

        currentIndex = index;

        if (fitActiveBackgroundToCamera)
        {
            FitBackgroundToCamera(backgrounds[currentIndex]);
        }
    }

    public void NextBackground()
    {
        if (backgrounds == null || backgrounds.Length == 0)
            return;

        int nextIndex = currentIndex + 1;

        if (nextIndex >= backgrounds.Length)
            nextIndex = 0;

        SetBackground(nextIndex);
    }

    public void PreviousBackground()
    {
        if (backgrounds == null || backgrounds.Length == 0)
            return;

        int previousIndex = currentIndex - 1;

        if (previousIndex < 0)
            previousIndex = backgrounds.Length - 1;

        SetBackground(previousIndex);
    }

    public void SetBackgroundByName(string backgroundName)
    {
        if (backgrounds == null || backgrounds.Length == 0)
            return;

        for (int i = 0; i < backgrounds.Length; i++)
        {
            if (backgrounds[i] != null && backgrounds[i].name == backgroundName)
            {
                SetBackground(i);
                return;
            }
        }

        Debug.LogWarning("Background nicht gefunden: " + backgroundName);
    }

    private void FitBackgroundToCamera(GameObject backgroundObject)
    {
        if (backgroundObject == null)
            return;

        if (Camera.main == null)
        {
            Debug.LogWarning("Keine Main Camera gefunden.");
            return;
        }

        SpriteRenderer spriteRenderer = backgroundObject.GetComponentInChildren<SpriteRenderer>();

        if (spriteRenderer == null || spriteRenderer.sprite == null)
        {
            Debug.LogWarning("Kein SpriteRenderer oder Sprite im Background gefunden: " + backgroundObject.name);
            return;
        }

        float screenHeight = Camera.main.orthographicSize * 2f;
        float screenWidth = screenHeight * Camera.main.aspect;

        Vector2 spriteSize = spriteRenderer.sprite.bounds.size;

        float scaleX = screenWidth / spriteSize.x;
        float scaleY = screenHeight / spriteSize.y;

        // Cover: Füllt den ganzen Bildschirm.
        // Dabei kann je nach Seitenverhältnis etwas abgeschnitten werden.
        float scale = Mathf.Max(scaleX, scaleY);

        backgroundObject.transform.localScale = new Vector3(scale, scale, 1f);

        Vector3 cameraPosition = Camera.main.transform.position;

        backgroundObject.transform.position = new Vector3(
            cameraPosition.x,
            cameraPosition.y,
            backgroundObject.transform.position.z
        );
    }
}