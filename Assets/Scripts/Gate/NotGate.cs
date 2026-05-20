using UnityEngine;
using System.Collections.Generic;

public class NotGate : BasicGate
{
    public override int CalculateOutput(List<int> inputs)
    {
        Debug.Log("Calculating NOT gate output with inputs: " + string.Join(", ", inputs));
        if (inputs.Count != 1)
            return -1;
        return (inputs[0] == 0) ? 1 : 0;
    }

}
