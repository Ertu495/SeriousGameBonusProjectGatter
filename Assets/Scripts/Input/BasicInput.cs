using UnityEngine;
using TMPro;
public class BasicInput : MonoBehaviour
{
    public bool locked = false;
    public int value = -1;

    public TextMeshPro textMesh;

    Vector3 mousePositionOffset;

    public bool isDragging = false;


    // based on this tutorial https://www.youtube.com/watch?v=yalbvB84kCg
    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePoint = Input.mousePosition;

        mousePoint.z = Camera.main.WorldToScreenPoint(transform.position).z;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void OnMouseDown()
    {
        if (locked) return;

        mousePositionOffset = transform.position - GetMouseWorldPosition();
    }

    private void OnMouseDrag()
    {
        if (locked) return;

        isDragging = true;
        transform.position = GetMouseWorldPosition() + mousePositionOffset;
    }

    private void OnMouseUp()
    {
        isDragging = false;
    }

    void Start()
    {
        if (value == -1)
            textMesh.text = "?";
        else
            textMesh.text = value.ToString();
    }

}
