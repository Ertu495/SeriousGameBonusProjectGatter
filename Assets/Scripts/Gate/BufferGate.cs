using UnityEngine;
using System.Collections.Generic;

public class BufferGate : BasicGate
{
    void Awake()
    {
        gateType = GateType.OneInput;
        requiredInputs = 1;
        gateName = "BUFFER";
    }

    public override int CalculateOutput(List<int> inputs)
    {
        return inputs.Count == 1 ? inputs[0] : -1;
    }
}
