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
        Inventory inventory = FindObjectOfType<Inventory>();

        if (inventory == null)
        {
            Debug.LogError("Inventory not found in the scene.");
            return false;
        }
        return inventory.CountItem("Coin") >= requiredCoinCount;
    }
}
