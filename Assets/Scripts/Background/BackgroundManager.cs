using System.Collections;
using UnityEngine;

public class BackgroundObjectManager : MonoBehaviour
{
    [Header("Background Objects")]
    [SerializeField] private GameObject[] backgrounds;

    [Header("Start Settings")]
    [SerializeField] private int startBackgroundIndex = 0;

    [Header("Intro Settings")]
    [SerializeField] private float introBackgroundDuration = 2f;
    [SerializeField] private int backgroundStartIndex = 0;
    [SerializeField] private int backgroundEndIndex = 0;
    private bool introPlayed = false;

    [Header("Scaling")]
    [SerializeField] private bool fitActiveBackgroundToCamera = true;

    private int currentIndex = -1;
    private Coroutine introCoroutine;

    private void Awake()
    {
        InitializeBackgrounds();
        SetBackground(startBackgroundIndex);
    }


    private void InitializeBackgrounds()
    {
        if (backgrounds != null && backgrounds.Length > 0)
            return;

        backgrounds = new GameObject[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            backgrounds[i] = transform.GetChild(i).gameObject;
        }
    }

    public void SetBackground(int index)
    {
        if (backgrounds == null || backgrounds.Length == 0)
        {
            Debug.LogWarning("No backgrounds assigned");
            return;
        }

        if (index < 0 || index >= backgrounds.Length)
        {
            Debug.LogWarning("Invalid background index: " + index);
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

    public void PlayIntro()
    {
        if (introPlayed) return;
        if (introCoroutine != null)
        {
            StopCoroutine(introCoroutine);
        }

        introCoroutine = StartCoroutine(PlayIntroRoutine());
        introPlayed = true;
    }

    public void StopIntro()
    {
        if (introCoroutine != null)
        {
            StopCoroutine(introCoroutine);
            introCoroutine = null;
        }
    }

    private IEnumerator PlayIntroRoutine()
    {
        if (backgrounds == null || backgrounds.Length == 0)
        {
            Debug.LogWarning("No backgrounds assigned");
            yield break;
        }


        for (int i = backgroundStartIndex; i <= backgroundEndIndex; i++)
        {
             SetBackground(i);
             yield return new WaitForSeconds(introBackgroundDuration);
        }


        introCoroutine = null;
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

    private void FitBackgroundToCamera(GameObject backgroundObject)
    {
        if (backgroundObject == null)
            return;

        if (Camera.main == null)
        {
            Debug.LogWarning("No Main Camera found.");
            return;
        }

        SpriteRenderer spriteRenderer = backgroundObject.GetComponentInChildren<SpriteRenderer>();

        if (spriteRenderer == null || spriteRenderer.sprite == null)
        {
            Debug.LogWarning("No SpriteRenderer or Sprite found in background: " + backgroundObject.name);
            return;
        }

        float screenHeight = Camera.main.orthographicSize * 2f;
        float screenWidth = screenHeight * Camera.main.aspect;

        Vector2 spriteSize = spriteRenderer.sprite.bounds.size;

        float scaleX = screenWidth / spriteSize.x;
        float scaleY = screenHeight / spriteSize.y;

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