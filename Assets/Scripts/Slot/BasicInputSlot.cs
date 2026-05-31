using UnityEngine;
using System.Collections.Generic;

public class BasicInputSlot : BasicSlot
{
    private GameObject currentInput;
    public GameObject[] valueVisuals;
    public int lockedValue = 0;


    void Update()
    {
        if (locked)
        {
            SetOutput(lockedValue);
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (locked) return;
        BasicInput input = other.GetComponent<BasicInput>();
        if (input == null) return;
        if (input.isDragging) return;

        currentInput = input.gameObject;
        currentInput.transform.position = transform.position;

        SetOutput(input.value);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (locked) return;

        SetOutput(-1);
    }
}