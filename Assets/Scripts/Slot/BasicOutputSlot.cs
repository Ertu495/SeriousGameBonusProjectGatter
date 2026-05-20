using UnityEngine;
using System.Collections.Generic;



public class BasicOutputSlot : BasicSlot
{
    public GameObject currentInput;

    private void OnTriggerStay2D(Collider2D other)
    {
        BasicInput input = other.GetComponent<BasicInput>();
        if (input == null) return;
        if (locked) return;
        if (input.isDragging) return;

        if (currentInput != input.gameObject)
        {
            if (currentInput != null)
            {
                currentInput.transform.position = transform.position + new Vector3(0, -3f, 0);
            }

            currentInput = input.gameObject;
        }

        currentInput.transform.position = transform.position;
        output = input.value;
        locked = input.locked;
        lockedText.text = locked ? "Locked" : "";

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        BasicInput input = other.GetComponent<BasicInput>();
        if (input == null) return;
        if (locked) return;

        if (currentInput == input.gameObject)
        {
            currentInput = null;
            output = -1;
        }
    }
}