using UnityEngine;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{

    public GameObject[] Levels;
    public GameObject tutorialTextObject; 
    public GameObject endLevelObject; 


    public void CreateLevel(int levelNumber)
    {
        GameObject levelObj = Instantiate(
            Levels[levelNumber],
            Vector3.zero,
            Quaternion.identity
        );

        Level level = levelObj.GetComponent<Level>();

        if (level != null)
        {
            level.tutorialTextObject = tutorialTextObject;
            level.endLevelObject = endLevelObject;
            level.LevelManagerObject = this.gameObject;
            level.levelNumber = levelNumber;
            level.StartLevel();
        }
    }

    public void DestroyLevel()
    {
        
        foreach (var level in FindObjectsOfType<Level>())
        {
            Destroy(level.gameObject);
        }

        foreach (var cable in FindObjectsOfType<StraightCable>())
        {
            Destroy(cable.gameObject);
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
}
