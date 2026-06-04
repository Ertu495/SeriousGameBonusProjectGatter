using UnityEngine;
using System.Collections.Generic;

public class AndGate : BasicGate
{
    void Awake()
    {
        gateType = GateType.TwoInput;
        requiredInputs = 2;
        gateName = "AND";
    }

    public override int CalculateOutput(List<int> inputs)
    {
        return inputs.Count >= 2 && inputs[0] == 1 && inputs[1] == 1 ? 1 : 0;
    }
}