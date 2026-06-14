using UnityEngine;
using TMPro;

public class BasicInput : DraggableObject
{
    public int value = -1;
    public TextMeshPro textMesh;
    public BasicInputSlot currentSlot;

    void Start()
    {
       // textMesh.text = value == -1 ? "?" : value.ToString();
    }

    protected override bool IsInAnySlot()
    {
        return currentSlot != null;
    }
}