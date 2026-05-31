using UnityEngine;
using System.Collections.Generic;

public class BasicGateSlot : BasicSlot
{
    public GameObject currentGate;
    public BasicGate gateInside;
    public BasicGate.GateType requiredGateType;

    private bool IsWrongGateType(BasicGate gate)
    {
        return gate.gateType != requiredGateType;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        BasicGate gate = other.GetComponent<BasicGate>();
        if (gate == null) return;
        if (locked) return;
        //if (gate.isDragging) return;

        if (gateInside == null)
        {
            if (IsWrongGateType(gate)) return;
        }
        else
        {
            if (IsWrongGateType(gateInside)) return;
        }


        if (currentGate != gate.gameObject)
        {
            if (currentGate != null)
            {
                currentGate.transform.position = transform.position + new Vector3(0, -3f, 0);
            }
            currentGate = gate.gameObject;
            gateInside = currentGate.GetComponent<BasicGate>();
            gate.isInSlot = true;

            Recalculate();

        }

        currentGate.transform.position = transform.position;
        locked = gate.locked;
        lockedText.text = locked ? "Locked" : "";
    }

    private void Recalculate()
    {
        if (currentGate == null)
        {
            SetOutput(-1);
            return;
        }

        if (gateInside == null)

            return;
        List<int> inputs = new List<int>(receivedInputs.Values);
        if (inputs.Count > 0)
        {
            SetOutput(gateInside.CalculateOutput(inputs));
        }
        else
        {
            SetOutput(-1);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        BasicGate gate = other.GetComponent<BasicGate>();
        if (gate == null) return;
        if (locked) return;

        if (currentGate == gate.gameObject)
        {
            gate.isInSlot = false;
            currentGate = null;
            gateInside = null;
            Recalculate();

        }
    }



    public override void ReceiveValue(StraightCable cable, int value)
    {
        if (gateInside != null &&
            receivedInputs.Count >= gateInside.requiredInputs)
        {
            return;
        }

        base.ReceiveValue(cable, value);
        Recalculate();
    }

    private void UpdateDebugList()
    {
        debugInputs.Clear();

        foreach (var kvp in receivedInputs)
        {
            debugInputs.Add(new CableInputDebug
            {
                cable = kvp.Key,
                value = kvp.Value
            });
        }
    }

}