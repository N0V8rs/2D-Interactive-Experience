using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public string Description;
    public bool IsCompleted;
    public bool IsGiven;

    public virtual bool CheckCompletionCondition(PlayerController player)
    {
        return false;
    }
}
