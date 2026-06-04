using UnityEngine;

public abstract class BaseDropSlot : MonoBehaviour
{
    public bool locked;

    public abstract bool CanAccept(DraggableObject obj);
    public abstract void OnDrop(DraggableObject obj);

    // 🔥 NEU: wichtig für sauberes Entfernen
    public virtual void OnObjectRemoved(DraggableObject obj) { }

    public virtual void ReceiveValue(StraightCable cable, int value) { }
}