using UnityEngine;

public abstract class DraggableObject : MonoBehaviour
{
    public bool locked = false;
    public bool isDragging = false;

    private bool externalDrag = false;
    protected Vector3 mouseOffset;

    private SpriteRenderer[] renderers;
    private int[] originalOrders;

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

        ResetSorting();

        if (!hit)
        {
            if (this is BasicGate g && g.currentSlot != null)
                g.currentSlot.ForceRemoveGate(g);

            Destroy(gameObject);
        }
    }

    protected void ResetSorting()
    {
        if (renderers == null) return;

        for (int i = 0; i < renderers.Length; i++)
        {
            if (renderers[i] == null) continue;
            renderers[i].sortingOrder = 2;
        }
    }

    public void ForceStartDrag()
    {
        isDragging = true;
        externalDrag = true;

        mouseOffset = transform.position - GetMouseWorldPosition();

        renderers = GetComponentsInChildren<SpriteRenderer>();
        originalOrders = new int[renderers.Length];

        for (int i = 0; i < renderers.Length; i++)
        {
            if (renderers[i] == null) continue;

            originalOrders[i] = renderers[i].sortingOrder;
            renderers[i].sortingOrder = 1000;
        }
    }

    protected abstract bool IsInAnySlot();
}