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
        switch (item.itemType)
        {
            case "Potion":
                // Add a potion to the inventory'
                items.Add(item);
                Debug.Log("Potion Type");
                Debug.Log("Current Potion count: " + CountPotions());
                break;

            case "Sword":
                // Add a potion to the inventory'
                items.Add(item);
                Debug.Log("Sword Type");
                Debug.Log("Current Sword count: " + CountSword());
                break;

            case "Coin":
                // Add a potion to the inventory'
                items.Add(item);
                Debug.Log("Coin Type");
                Debug.Log("Current Coin count: " + CountCoin());
                break;

            case "KeyShards":
                items.Add(item);
                Debug.Log("KeyShards Type");
                FindObjectOfType<QuestManager>().UpdateKeyShards();

                if (KeyShards() == 3)
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

                if (KeyShards() == 5)
                {
                    GameObject parentObject = GameObject.Find("LockedDoor");
                    if (parentObject != null)
                    {
                        Transform childObject = parentObject.transform.Find("UnlockedDoor");
                        if (childObject != null)
                        {
                            childObject.gameObject.SetActive(true);

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

                break;

            case "Weapon":
                // Add a Weapon to the inventory
                items.Add(item);
                Debug.Log("Weapon Type");
                break;

            case "Statue":
                // Statue interacted with
                items.Add(item);
                Debug.Log("Statue Type");
                break;

            default:
                Debug.LogError("Invalid item type: " + item.itemType);
                break;
        }
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
}