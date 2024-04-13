using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionQuest : Quest
{
    public int requiredPotionCount = 5;
    public override bool CheckCompletionCondition(PlayerController player)
    {
        Inventory inventory = FindObjectOfType<Inventory>();

        if (inventory == null)
        {
            Debug.LogError("Inventory not found in the scene.");
            return false;
        }
        return inventory.CountItem("Potion") >= requiredPotionCount;
    }
}
