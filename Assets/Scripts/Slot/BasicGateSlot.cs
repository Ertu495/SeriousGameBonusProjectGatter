using UnityEngine;
using System.Collections.Generic;

public class BasicGateSlot : BasicSlot
{
    public BasicGate currentGate;
    public BasicGate.GateType requiredGateType;

    [Header("Visuals")]
    public GameObject gateVisual;

    public Dictionary<StraightCable, int> receivedInputs = new();

    // =========================
    // DROP
    // =========================
    public void OnDrop(DraggableObject obj)
    {
        if (obj is not BasicGate gate) return;

        if (locked || gate.gateType != requiredGateType)
        {
            Destroy(gate.gameObject);
            return;
        }

        SetGate(gate);
    }

    // =========================
    // SINGLE SOURCE OF TRUTH
    // =========================
    private void SetGate(BasicGate newGate)
    {
        // remove old
        if (currentGate != null && currentGate != newGate)
        {
            var old = currentGate;

            old.currentSlot = null;
            currentGate = null;

            Destroy(old.gameObject);
        }

        currentGate = newGate;
        newGate.currentSlot = this;

        newGate.transform.position = transform.position;

        Recalculate();
        ApplyVisual();
    }

    // =========================
    // REMOVE (ONLY SLOT CONTROLS THIS)
    // =========================
    public void ForceRemoveGate(BasicGate gate)
    {
        if (currentGate != gate) return;

        gate.currentSlot = null;
        currentGate = null;

        Recalculate();
        ApplyVisual();
    }

    // =========================
    // INPUT
    // =========================
    public override void ReceiveValue(StraightCable cable, int value)
    {
        receivedInputs[cable] = value;
        Recalculate();
    }

    // =========================
    // LOGIC
    // =========================
    private void Recalculate()
    {
        if (currentGate == null)
        {
            SetOutput(-1);
            ApplyVisual(); // 🔥 ADD
            return;
        }

        CleanInputs();

        var inputs = new List<int>(receivedInputs.Values);

        if (inputs.Count < currentGate.requiredInputs)
        {
            SetOutput(-1);
            ApplyVisual(); // 🔥 ADD
            return;
        }

        foreach (var v in inputs)
        {
            if (v == -1)
            {
                SetOutput(-1);
                ApplyVisual(); // 🔥 ADD
                return;
            }
        }

        SetOutput(currentGate.CalculateOutput(inputs));

        ApplyVisual(); // 🔥 ADD (WICHTIG)
    }

    private void CleanInputs()
    {
        List<StraightCable> remove = new();

        foreach (var p in receivedInputs)
        {
            if (p.Key == null)
                remove.Add(p.Key);
        }

        foreach (var r in remove)
            receivedInputs.Remove(r);
    }

    // =========================
    // VISUAL (SAFE)
    // =========================
    private void ApplyVisual()
    {
        if (gateVisual == null) return;

        gateVisual.SetActive(true);

        // HARD BLOCK interaction
        var drag = gateVisual.GetComponent<DraggableObject>();
        if (drag != null)
            drag.enabled = false;

        var col = gateVisual.GetComponent<Collider2D>();
        if (col != null)
            col.enabled = false;
    }
}