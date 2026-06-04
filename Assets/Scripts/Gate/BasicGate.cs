using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class BasicGate : DraggableObject
{
    public string gateName = "Gate";
    public TextMeshPro gateText;

    public enum GateType { OneInput, TwoInput }
    public GateType gateType;

    public BasicGateSlot currentSlot;
    public int requiredInputs = 0;

    void Start()
    {
        if (gateText != null)
            gateText.text = gateName;
    }

    public virtual int CalculateOutput(List<int> inputs)
    {
        return -1;
    }

    protected override bool IsInAnySlot()
    {
        return currentSlot != null;
    }

}