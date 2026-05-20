using UnityEngine;
using TMPro;
public class BasicInput : MonoBehaviour
{
    public bool locked = false;
    public int value = 0;

    public TextMeshPro textMesh;

    Vector3 mousePositionOffset;
    
    public bool isDragging = false;

        /// based on this tutorial https://www.youtube.com/watch?v=yalbvB84kCg
    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePoint = Input.mousePosition;

        mousePoint.z = Camera.main.WorldToScreenPoint(transform.position).z;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
        
    private void OnMouseDown()
    {
        if (!locked)
        {
            mousePositionOffset = gameObject.transform.position - GetMouseWorldPosition();
        }
    }

    private void OnMouseDrag()
    {
        if (!locked)
        {
            isDragging = true;
            transform.position = GetMouseWorldPosition() + mousePositionOffset;
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;
    }

    void Start()
    {
        textMesh.text = value.ToString();
    }

}
