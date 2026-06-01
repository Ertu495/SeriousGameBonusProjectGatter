using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class BasicOutputSlot : BasicSlot
{
    public GameObject currentInput;
    public int targetValue;
    public TextMeshPro targetText;
    public TextMeshPro outputText;
    public SpriteRenderer background;
    public bool isSolved = false;

    void Start()
    {
        UpdateTarget();
        UpdateOutput(-1);
    }

    void UpdateTarget()
    {
        targetText.text = "Target: " + targetValue;
    }
    void ShowLevelSummary()
    {
        Level level = FindFirstObjectByType<Level>();
        level.EndLevel();
    }

    void OnSolved()
    {
        if (isSolved) return;

        isSolved = true;

        LockAll();

        foreach (var cable in FindObjectsOfType<StraightCable>())
        {
            cable.SetSolved();
        }
        Invoke(nameof(ShowLevelSummary), 3f);

    }

    void LockAll()
    {
        BasicInput[] inputs = FindObjectsOfType<BasicInput>();
        foreach (var i in inputs)
        {
            i.locked = true;
        }

        BasicGate[] gates = FindObjectsOfType<BasicGate>();
        foreach (var g in gates)
        {
            g.locked = true;
        }
    }

    void UpdateOutput(int value)
    {
        if (value == -1)
        {
            outputText.text = "?";
            background.color = Color.gray;
            return;
        }

        outputText.text = value.ToString();

        if (value == targetValue)
        {
            background.color = Color.green;
            OnSolved();
        }
        else
        {
            background.color = Color.red;
        }
    }

    public void SetInput(int value)
    {
        UpdateOutput(value);
    }

    public override void ReceiveValue(StraightCable cable, int value)
    {
        SetInput(value);
    }

}