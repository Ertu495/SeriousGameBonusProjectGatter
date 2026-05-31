using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class StraightCable : MonoBehaviour
{
    private Transform start;
    private Transform end;

    public BasicSlot sourceSlot;

    private LineRenderer lr;

    private int lastValue = -999;
    private float anim = 0f;
    private bool animate = false;
    public bool isSolved = false;

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    public void StartWinAnimation()
    {
        animate = true;
        anim = 0f;
    }

    void AnimateWinCable()
    {
        anim += Time.deltaTime;

        float t = Mathf.Clamp01(anim);

        Color startColor = new Color(0.2f, 0.5f, 0.2f);
        Color endColor = Color.green;

        Color current = Color.Lerp(startColor, endColor, t);

        lr.startColor = current;
        lr.endColor = current;
    }

    public void SetSolved()
    {
        isSolved = true;
    }

    public void Connect(Transform s, Transform e, BasicSlot slot)
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

        if (isSolved)
        {
            AnimateWinCable();
            return;
        }

        int value = sourceSlot != null ? sourceSlot.output : -1;

        Color c = GetColor(sourceSlot != null ? sourceSlot.output : -1);
        lr.startColor = c;
        lr.endColor = c;

        if (value != lastValue)
        {
            lastValue = value;
            RefreshSignal();
        }
    }

    public void RefreshSignal()
    {
        int value = sourceSlot != null ? sourceSlot.output : -1;


        BasicSlot slot = end.GetComponentInParent<BasicSlot>();
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