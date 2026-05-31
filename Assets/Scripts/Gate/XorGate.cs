using UnityEngine;
using System.Collections.Generic;

public class XorGate : BasicGate
{
    void Awake()
    {
        gateType = GateType.TwoInput;
        requiredInputs = 2;
    }
    public override int CalculateOutput(List<int> inputs)
    {
        if (inputs.Count < 2)
            return -1;
        return (inputs[0] != inputs[1]) ? 1 : 0;
    }
}