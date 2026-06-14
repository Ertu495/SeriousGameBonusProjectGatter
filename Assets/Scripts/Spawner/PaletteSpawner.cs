using UnityEngine;

public class PaletteSpawner : MonoBehaviour
{
    public GameObject prefab;
    public bool locked;

    private GameObject spawned;

    private void OnMouseDown()
    {
        if (locked) return;

        spawned = Instantiate(prefab);
        spawned.transform.position = GetMouseWorldPosition();

        foreach (var sr in spawned.GetComponentsInChildren<SpriteRenderer>())
        {
            sr.sortingOrder = 1000;
        }

        var drag = spawned.GetComponent<DraggableObject>();
        if (drag != null)
            drag.ForceStartDrag();
    }

    Vector3 GetMouseWorldPosition()
    {
        Vector3 m = Input.mousePosition;
        m.z = 10f;
        return Camera.main.ScreenToWorldPoint(m);
    }
}