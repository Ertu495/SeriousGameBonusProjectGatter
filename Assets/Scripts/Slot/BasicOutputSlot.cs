using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class BasicOutputSlot : BasicSlot
{

    public GameObject currentInput;
    public int targetValue;
    public TextMeshPro targetText;
    public TextMeshPro outputText;
    public bool isSolved = false;
    private int lastValue = -2;
    public GameObject[] valueVisuals;

    void Start()
    {
        UpdateTarget();
        UpdateOutput(-1);
    }

    void UpdateTarget()
    {
        targetText.text = targetValue.ToString();
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
        PaletteSpawner[] spawners = FindObjectsOfType<PaletteSpawner>();
        foreach (var s in spawners)
        {
            s.locked = true;
        }
    }

void UpdateOutput(int value)
    {
        // SICHERHEITS-CHECK: Ist der Wert noch exakt derselbe wie im Frame davor? 
        // Wenn ja -> brich hier sofort ab! Kein Spamming mehr!
        if (value == lastValue) return; 

        // Es ist ein neuer Wert! Wir merken uns diesen für den nächsten Frame.
        lastValue = value;

        if (value == -1)
        {
            for (int i = 0; i < valueVisuals.Length; i++)
            {
                if (valueVisuals[i] != null)
                    valueVisuals[i].SetActive(false);
            }

            return;
        }

        if (valueVisuals == null) return;

        for (int i = 0; i < valueVisuals.Length; i++)
            if (valueVisuals[i] != null)
                valueVisuals[i].SetActive(i == value);

        if (value != targetValue)
        {
            Debug.Log("wrong");
            // Spiele den Sound nur ab, wenn er nicht ohnehin gerade schon läuft
            if (!GameObject.Find("wrongSound").GetComponent<AudioSource>().isPlaying)
            {
                GameObject.Find("wrongSound").GetComponent<AudioSource>().Play(); 
            }
        } 
        else
        {
            OnSolved();
            Debug.Log("success");
            if (!GameObject.Find("rightSound").GetComponent<AudioSource>().isPlaying)
            {
                GameObject.Find("rightSound").GetComponent<AudioSource>().Play();
            }
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

    void ShowLevelSummary()
    {
        Level level = FindFirstObjectByType<Level>();
        level.EndLevel();
    }
}