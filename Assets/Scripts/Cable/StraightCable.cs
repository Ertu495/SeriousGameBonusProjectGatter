using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class StraightCable : MonoBehaviour
{
    private Transform start;
    private Transform end;

    public BasicSlot sourceSlot;

    private LineRenderer lr;

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    public void Connect(Transform s, Transform e,  BasicSlot slot)
    {
        start = s;
        end = e;
        sourceSlot = slot;
    }

    void Update()
    {
        if (start == null || end == null) return;

        lr.positionCount = 2;
        lr.SetPosition(0, start.position);
        lr.SetPosition(1, end.position);
        int value = sourceSlot != null ? sourceSlot.output : -1;

        Color c = GetColor(sourceSlot != null ? sourceSlot.output : -1);
        lr.startColor = c;
        lr.endColor = c;
    }

    public void RefreshSignal()
    {
        int value = sourceSlot != null ? sourceSlot.output : -1;
        BasicSlot slot = end.GetComponentInParent<BasicGateSlot>();
        if (slot != null)
        {
            slot.ReceiveValue(this, value);
        }
    }

    Color GetColor(int v)
    {
        return v switch
        {
            -1 => Color.red,
            0 => Color.gray,
            1 => Color.yellow,
            2 => Color.green,
        };
    }
}