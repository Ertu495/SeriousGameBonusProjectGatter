using UnityEngine;
using System.Collections.Generic;

public class AndGate : BasicGate
{
    public override int CalculateOutput(List<int> inputs)
    {
        Debug.Log("Calculating And gate output with inputs: " + string.Join(", ", inputs));
        if (inputs.Count < 2)
            return -1;
        return (inputs[0] == 1 && inputs[1] == 1) ? 1 : 0;
    }
}