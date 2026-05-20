using UnityEngine;
using System.Collections.Generic;

public class OrGate : BasicGate
{
    public override int CalculateOutput(List<int> inputs)
    {
        if (inputs.Count < 2)
            return -1;
        return (inputs[0] == 1 || inputs[1] == 1) ? 1 : 0;
    }
}