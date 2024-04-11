using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UnityEditor.Progress;

public class InteractionObject : MonoBehaviour
{
    [Header("Info")]
    public bool info;
    public string message;
    public TextMeshPro infoText;
    public string interactionType;

    [Header("Pickups")]
    public bool pickup;
    public Item item;

    [Header("Dialogue")]
    public bool talking;
    public string[] dialogueLines;
    private int currentDialogueLine = 0;

    public void Start()
    {

    }

    public void Info()
    {
        // Log message
        Debug.Log(message);

        if (infoText != null)
        {
            infoText.text = message;
            StartCoroutine(FadeText());
        }
        else
        {
            Debug.LogError("infoText is not assigned!");
        }

        if (interactionType == "WeaponInfo")
        {
            // Add a weapon to the player's inventory
            FindObjectOfType<PlayerController>().Inventory.AddItem(new Item { itemType = "Weapon" });
        }

        if (interactionType == "StatueInteract")
        {
            // Add a weapon to the player's inventory
            FindObjectOfType<PlayerController>().Inventory.AddItem(new Item { itemType = "Statue" });
        }
    }

    public void Pickup()
    {
        Debug.Log("You Picked Up " + this.gameObject.name);
        PlayerController playerController = PlayerController.Instance;

        // Check if the Inventory property is null
        if (playerController.Inventory == null)
        {
            Debug.LogError("Inventory not found.");
            return;
        }

        // Check if the item field is null
        if (this.item == null)
        {
            Debug.LogError("Item not found.");
            return;
        }

        FindObjectOfType<PlayerController>().Inventory.AddItem(new Item { itemType = this.item.itemType });

        this.gameObject.SetActive(false);

    }

    public void Dialogue()
    {
        Quest quest = GetComponent<Quest>();
        QuestManager questManager = FindObjectOfType<QuestManager>();
        Inventory playerInventory = FindObjectOfType<PlayerController>().Inventory;

        if (quest != null && !quest.IsGiven && currentDialogueLine >= dialogueLines.Length - 1)
        {

            questManager.AddQuest(quest);
            quest.IsGiven = true;
        }
        else if (quest != null && quest.IsGiven && !quest.IsCompleted && quest.CheckCompletionCondition(FindObjectOfType<PlayerController>()))
        {

            dialogueLines[0] = "Thank you so much! Here's a Stone to help you on your journey!";
            quest.IsCompleted = true;

            if (quest is PotionQuest)
            {
                playerInventory.RemoveAllItems();
            }
            Debug.Log("Current potion count: " + playerInventory.CountPotions());

            FindObjectOfType<PlayerController>().Inventory.AddItem(item: new Item { itemType = "Stone" });
            currentDialogueLine = 0;
            questManager.UpdateQuestText();

        }
        else if (quest != null && quest.IsGiven && !quest.IsCompleted)
        {

            dialogueLines[0] = "How is that task going? Be sure to come back once you're done!";
        }

        FindObjectOfType<DialogueManager>().StartDialogue(dialogueLines);
        currentDialogueLine++;
    }

    IEnumerator FadeText()
    {
        if (infoText == null)
        {
            Debug.LogError("infoText is not assigned!");
            yield break;
        }

        Color initialColor = infoText.color;

        for (float t = 0; t < 1; t += Time.deltaTime / 2f)
        {
            float alpha = Mathf.Lerp(0, 1, t);
            infoText.color = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);
            yield return null;
        }

        yield return new WaitForSeconds(3f);

        for (float t = 0; t < 1; t += Time.deltaTime / 2f)
        {
            float alpha = Mathf.Lerp(1, 0, t);
            infoText.color = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);
            yield return null;
        }
        infoText.text = null;
    }
}