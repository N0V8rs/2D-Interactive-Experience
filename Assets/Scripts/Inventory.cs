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
                Debug.Log("Current potion count: " + CountPotions());
                break;

            case "Debris":
                // Add a potion to the inventory'
                items.Add(item);
                Debug.Log("Debris Type");
                Debug.Log("Current Debris count: " + CountDebris());
                break;

            case "Soul":
                // Add a potion to the inventory'
                items.Add(item);
                Debug.Log("Soul Type");
                Debug.Log("Current Soul count: " + CountSouls());
                break;

            case "Stone":
                // Add a stone to the inventory
                items.Add(item);
                Debug.Log("Stone Type");
                FindObjectOfType<QuestManager>().UpdateStoneCountText();

                if (CountStones() == 3)
                {
                    // If it has, find the parent game object
                    GameObject parentObject = GameObject.Find("Portal");
                    if (parentObject != null)
                    {
                        // Find the child game object and activate it
                        Transform childObject = parentObject.transform.Find("GoodPortal");
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

                if (CountStones() == 5)
                {
                    // Find the parent game object for the second portal
                    GameObject parentObject = GameObject.Find("Portal");
                    if (parentObject != null)
                    {
                        // Find the child game object and activate it
                        Transform childObject = parentObject.transform.Find("GoodPortal");
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

    public int CountStones()
    {
        return items.Count(i => i.itemType == "Stone");
    }

    public int CountWeapons()
    {
        return items.Count(i => i.itemType == "Weapon");
    }

    public int CountPotions()
    {
        return items.Count(i => i.itemType == "Potion");
    }

    public int CountDebris()
    {
        return items.Count(i => i.itemType == "Debris");
    }

    public int CountSouls()
    {
        return items.Count(i => i.itemType == "Soul");
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