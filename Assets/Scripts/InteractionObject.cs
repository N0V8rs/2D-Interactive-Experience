using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InteractionObject : MonoBehaviour
{
    [Header("Info")]
    public bool info;
    public string message;
    public TMP_Text infoText;
    public string interactionType;

    [Header("Pickups")]
    public bool pickup;
    public Item item;

    [Header("Dialogue")]
    public bool talking;
    public string[] dialogueLines;
    private int currentDialogueLine = 0;

    private PlayerController playerController;
    private Inventory playerInventory;
    private QuestManager questManager;

    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
        playerInventory = playerController.Inventory;
        questManager = FindObjectOfType<QuestManager>();
    }

    public void Info()
    {
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

        if (interactionType == "WeaponInfo" || interactionType == "StatueInteract")
        {
            playerInventory.AddItem(new Item { itemType = interactionType.Replace("Info", "").Replace("Interact", "") });
        }
    }

    public void Pickup()
    {
        Debug.Log("You Picked Up " + this.gameObject.name);

        // Check if the item field is null
        if (this.item == null)
        {
            Debug.LogError("Item not found.");
            return;
        }

        playerInventory.AddItem(new Item { itemType = this.item.itemType });

        this.gameObject.SetActive(false);
    }

    public void Dialogue()
    {
        Quest quest = GetComponent<Quest>();

        if (quest != null && !quest.IsGiven && currentDialogueLine >= dialogueLines.Length - 1)
        {
            questManager.AddQuest(quest);
            quest.IsGiven = true;
        }
        else if (quest != null && quest.IsGiven && !quest.IsCompleted && quest.CheckCompletionCondition(playerController))
        {
            CompleteQuest(quest);
        }
        else if (quest != null && quest.IsGiven && !quest.IsCompleted)
        {
            dialogueLines[0] = "How's the quest going, come back with the items I required";
        }

        FindObjectOfType<DialogueManager>().StartDialogue(dialogueLines);
        currentDialogueLine++;
    }

    private void CompleteQuest(Quest quest)
    {
        dialogueLines[0] = "Thank you for collecting for me, here is a key shard for your troubles";
        quest.IsCompleted = true;

        if (quest is PotionQuest)
        {
            playerInventory.RemoveAllItems();
        }
        Debug.Log("Current potion count: " + playerInventory.CountItem("Potion"));

        playerInventory.AddItem(item: new Item { itemType = "KeyShards" });
        currentDialogueLine = 0;
        questManager.UpdateQuestText();
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
