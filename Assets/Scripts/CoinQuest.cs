using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class CoinQuest : Quest
{
    public int requiredCoinCount = 3;

    public override bool CheckCompletionCondition(PlayerController player)
    {
        // Get the Inventory from the scene
        Inventory inventory = FindObjectOfType<Inventory>();

        // Check if the inventory is null
        if (inventory == null)
        {
            Debug.LogError("Inventory not found in the scene.");
            return false;
        }

        // Check if the player has collected enough potions
        return inventory.CountCoin() >= requiredCoinCount;
    }
}
