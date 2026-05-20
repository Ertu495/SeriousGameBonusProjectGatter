using UnityEngine;
using System.Collections.Generic;
using TMPro;


[System.Serializable]
public class CableInputDebug
{
    public StraightCable cable;
    public int value;
}

[System.Serializable]
public class CablePair
{
    public GameObject output;
    public GameObject input;
}

public class BasicSlot : MonoBehaviour
{
    public bool locked = false;

    public TextMeshPro lockedText;
    public int output = -1;


    [Header("Cable")]
    public GameObject cablePrefab;

    public List<CablePair> cablePairs = new List<CablePair>();
    public Dictionary<StraightCable, int> receivedInputs =
        new Dictionary<StraightCable, int>();

    public List<CableInputDebug> debugInputs = new List<CableInputDebug>();

    private List<StraightCable> activeCables = new List<StraightCable>();

    
    private void Start()
    {
        UpdateCables();
    }



    public virtual void ReceiveValue(StraightCable cable, int value)
    {
        receivedInputs[cable] = value;
         UpdateDebugList(); 
    }



    protected void SetOutput(int newValue)
    {
        output = newValue;
        foreach (var cable in activeCables)
        {
            cable.RefreshSignal();
        }
    }

    public void UpdateCables()
    {
        if (cablePairs == null) return;

        int needed = cablePairs.Count;

        while (activeCables.Count < needed)
        {
            GameObject obj = Instantiate(cablePrefab);
            activeCables.Add(obj.GetComponent<StraightCable>());
        }

        for (int i = 0; i < needed; i++)
        {
            var pair = cablePairs[i];

            if (pair.output == null || pair.input == null) continue;

            StraightCable cable = activeCables[i];

            cable.Connect(
                pair.output.transform,
                pair.input.transform,
                this
            );
        }
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