using UnityEngine;
using System.Collections.Generic;

public class BufferGate : BasicGate
{
    void Awake()
    {
        gateType = GateType.OneInput;
        requiredInputs = 1;
    }
    public override int CalculateOutput(List<int> inputs)
    {
        // Debug.Log("Calculating Buffer gate output with inputs: " + string.Join(", ", inputs));
        if (inputs.Count != 1)
            return -1;
        return inputs[0];
    }

}
