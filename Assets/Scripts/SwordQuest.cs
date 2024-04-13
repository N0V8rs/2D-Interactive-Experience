using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordQuest : Quest
{
    public int requiredSwordCount = 3;

    public override bool CheckCompletionCondition(PlayerController player)
    {
        Inventory inventory = FindObjectOfType<Inventory>();

        if (inventory == null)
        {
            Debug.LogError("Inventory not found in the scene.");
            return false;
        }
        return inventory.CountItem("Sword") >= requiredSwordCount;
    }
}
