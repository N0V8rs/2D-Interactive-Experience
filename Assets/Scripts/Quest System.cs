using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestSystem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[System.Serializable]
public class Quest
{
    public string questName;
    public string questDescription;
    public bool questCompleted;
    public int itemsRequired;
    public int itemsCollected;
}
