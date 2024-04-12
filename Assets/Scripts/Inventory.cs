using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<Item> items;

    public Inventory()
    {
        items = new List<Item>();
    }

    public int KeyShards()
    {
        return items.Count(i => i.itemType == "KeyShards");
    }

    public int CountWeapons()
    {
        return items.Count(i => i.itemType == "Weapon");
    }

    public int CountPotions()
    {
        return items.Count(i => i.itemType == "Potion");
    }

    public int CountSword()
    {
        return items.Count(i => i.itemType == "Sword");
    }

    public int CountCoin()
    {
        return items.Count(i => i.itemType == "Coin");
    }
    public int CountStatue()
    {
        return items.Count(i => i.itemType == "Statue");
    }

    public void RemoveItem(Item item)
    {
        items.Remove(item);
    }
    public void RemoveAllItems()
    {
        items.RemoveAll(item => item.itemType == "Potion");
    }
    public bool ContainsItem(Item item)
    {
        return items.Contains(item);
    }

    public int GetItemCount(Item item)
    {
        return items.Count(i => i == item);
    }
    public void AddItem(Item item)
    {
        items.Add(item);
        Debug.Log($"{item.itemType} Type");

        switch (item.itemType)
        {
            case "Potion":
                Debug.Log($"Current Potion count: {CountPotions()}");
                break;
            case "Sword":
                Debug.Log($"Current Sword count: {CountSword()}");
                break;
            case "Coin":
                Debug.Log($"Current Coin count: {CountCoin()}");
                break;
            case "KeyShards":
                FindObjectOfType<QuestManager>().UpdateKeyShards();
                HandleKeyShards();
                break;
            case "Weapon":
                break;
            case "Statue":
                break;
            default:
                Debug.LogError($"Invalid item type: {item.itemType}");
                break;
        }
    }

    private void HandleKeyShards()
    {
        int keyShardsCount = KeyShards();
        if (keyShardsCount == 3 || keyShardsCount == 5)
        {
            GameObject parentObject = GameObject.Find("LockedDoor");
            if (parentObject != null)
            {
                Transform childObject = parentObject.transform.Find("UnlockedDoor");
                if (childObject != null)
                {
                    childObject.gameObject.SetActive(true);

                    // Play the sound
                    AudioSource audioSource = childObject.GetComponent<AudioSource>();
                    if (audioSource != null)
                    {
                        audioSource.Play();
                    }
                }
                else
                {
                    Debug.LogError("Child game object not found");
                }
            }
        }
    }
}