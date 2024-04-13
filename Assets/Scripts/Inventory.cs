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

    public void AddItem(Item item)
    {
        items.Add(item);
        Debug.Log($"{item.itemType} Type");

        switch (item.itemType)
        {
            case "Potion":
            case "Sword":
            case "Coin":
                Debug.Log($"Current {item.itemType} count: {CountItem(item.itemType)}");
                break;
            case "KeyShards":
                FindObjectOfType<QuestManager>().UpdateKeyShards();
                HandleKeyShards();
                break;
            case "Weapon":
            case "Statue":
                break;
            default:
                Debug.LogError($"Invalid item type: {item.itemType}");
                break;
        }
    }

    private void HandleKeyShards()
    {
        int keyShardsCount = CountItem("KeyShards");
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

    public int CountItem(string itemType)
    {
        return items.Count(i => i.itemType == itemType);
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
}
