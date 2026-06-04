using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class CablePair
{
    public GameObject output;
    public GameObject input;
}

public class BasicSlot : MonoBehaviour
{
    public bool locked = false;
    public int output = -1;

    public GameObject cablePrefab;
    public List<CablePair> cablePairs = new();

    public Dictionary<StraightCable, int> receivedInputs = new();
    private List<StraightCable> activeCables = new();

    private void Start()
    {
        UpdateCables();
    }

    public virtual void ReceiveValue(StraightCable cable, int value)
    {
        receivedInputs[cable] = value;
    }

    public void SetOutput(int newValue)
    {
        output = newValue;

        foreach (var c in activeCables)
        {
            if (c != null)
                c.RefreshSignal();
        }
    }

    public void UpdateCables()
    {
        if (cablePairs == null) return;

        while (activeCables.Count < cablePairs.Count)
        {
            var obj = Instantiate(cablePrefab);
            activeCables.Add(obj.GetComponent<StraightCable>());
        }

        for (int i = 0; i < cablePairs.Count; i++)
        {
            var pair = cablePairs[i];
            if (pair.output == null || pair.input == null) continue;

            activeCables[i].Connect(
                pair.output.transform,
                pair.input.transform,
                this
            );
        }
    }


}