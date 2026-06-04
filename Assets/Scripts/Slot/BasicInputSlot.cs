using UnityEngine;

public class BasicInputSlot : BasicSlot
{
    private BasicInput currentInput;
    public int lockedValue = 0;
    public GameObject[] valueVisuals;

    private void Update()
    {
        if (locked)
        {
            SetOutput(lockedValue);
            UpdateVisuals(lockedValue);
        }
    }

    private void UpdateVisuals(int value)
    {
        if (valueVisuals == null) return;

        for (int i = 0; i < valueVisuals.Length; i++)
            if (valueVisuals[i] != null)
                valueVisuals[i].SetActive(i == value);
    }

    public void TrySnap(DraggableObject obj)
    {
        if (obj is not BasicInput input) return;

        if (locked)
        {
            Destroy(input.gameObject);
            return;
        }

        if (currentInput != null && currentInput != input)
            Destroy(currentInput.gameObject);

        currentInput = input;
        input.currentSlot = this;

        input.transform.position = transform.position;

        SetOutput(input.value);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var input = other.GetComponent<BasicInput>();
        if (input == null) return;

        if (currentInput == input)
        {
            currentInput.currentSlot = null;
            currentInput = null;

            SetOutput(-1);
        }
    }
}