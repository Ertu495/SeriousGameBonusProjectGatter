using UnityEngine;

public abstract class DraggableObject : MonoBehaviour
{
    public bool locked = false;
    public bool isDragging = false;

    private bool externalDrag = false;
    protected Vector3 mouseOffset;

    protected virtual void OnMouseDown()
    {
        if (locked) return;

        if (this is BasicGate gate && gate.currentSlot != null)
        {
            gate.currentSlot.ForceRemoveGate(gate);
            gate.currentSlot = null;
        }

        isDragging = true;
        externalDrag = true;

        mouseOffset = transform.position - GetMouseWorldPosition();
    }

    protected Vector3 GetMouseWorldPosition()
    {
        Vector3 m = Input.mousePosition;
        m.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(m);
    }

    void Update()
    {
        if (isDragging && Input.GetMouseButton(0))
            transform.position = GetMouseWorldPosition() + mouseOffset;

        if (externalDrag && Input.GetMouseButtonUp(0))
            EndDrag();
    }

    public void EndDrag()
    {
        isDragging = false;
        externalDrag = false;

        bool hit = false;

        var cols = Physics2D.OverlapCircleAll(transform.position, 0.25f);

        foreach (var c in cols)
        {
            if (this is BasicGate gate)
            {
                var slot = c.GetComponent<BasicGateSlot>();
                if (slot != null)
                {
                    slot.OnDrop(this);
                    hit = true;
                    break;
                }
            }

            if (this is BasicInput input)
            {
                var slot = c.GetComponent<BasicInputSlot>();
                if (slot != null)
                {
                    slot.TrySnap(this);
                    hit = true;
                    break;
                }
            }
        }

        if (!hit)
        {
            if (this is BasicGate g && g.currentSlot != null)
                g.currentSlot.ForceRemoveGate(g);

            Destroy(gameObject);
        }
    }

    protected abstract bool IsInAnySlot();

    public void ForceStartDrag()
    {
        isDragging = true;
        externalDrag = true;
        mouseOffset = transform.position - GetMouseWorldPosition();
    }
}