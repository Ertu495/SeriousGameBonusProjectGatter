using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System.Collections;
public class BasicGate : MonoBehaviour
{
    public bool locked = false;
    public string gateName = "Gate";

    public TextMeshPro gateText;

    Vector3 mousePositionOffset;

    public bool isDragging = false;
    public int requiredInputs = 0;
    private Vector3 startPosition;
    public bool isInSlot = false;


    public enum GateType
    {
        OneInput,
        TwoInput
    }

    public GateType gateType;

    void Start()
    {
        startPosition = transform.position;
    }

    private IEnumerator ReturnToStart()
    {
        float t = 0;
        Vector3 start = transform.position;

        while (t < 1)
        {
            t += Time.deltaTime * 5f;
            transform.position = Vector3.Lerp(start, startPosition, t);
            yield return null;
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mousePoint);
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

        if (!isInSlot)
        {
            StartCoroutine(ReturnToStart());
        }
    }


}
