using UnityEngine;
using System.Collections.Generic;
using TMPro;
public class BasicGate : MonoBehaviour
{
    public bool locked = false;
    public string gateName = "Gate";
    
    public TextMeshPro gateText;

    Vector3 mousePositionOffset;
    
    public bool isDragging = false;

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void Start()
    {
        gateText.text = gateName;
    }


    public virtual int CalculateOutput(List<int> inputs)
    {
        return -1;
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


}
