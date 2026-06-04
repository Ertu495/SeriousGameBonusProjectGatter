using UnityEngine;
using System.Collections.Generic;

public class NotGate : BasicGate
{
    void Awake()
    {
        gateType = GateType.OneInput;
        requiredInputs = 1;
        gateName = "NOT";
    }

    public override int CalculateOutput(List<int> inputs)
    {
        return inputs.Count == 1 ? (inputs[0] == 0 ? 1 : 0) : -1;
    }
}