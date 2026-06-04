using UnityEngine;
using System.Collections.Generic;

public class OrGate : BasicGate
{
    void Awake()
    {
        gateType = GateType.TwoInput;
        requiredInputs = 2;
        gateName = "OR";
    }

    public override int CalculateOutput(List<int> inputs)
    {
        return inputs.Count >= 2 && (inputs[0] == 1 || inputs[1] == 1) ? 1 : 0;
    }
}