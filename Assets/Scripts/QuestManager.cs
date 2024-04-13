using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public List<Quest> ActiveQuests;
    public TMP_Text questText;
    public TMP_Text keyshardsText;

    private PlayerController playerController;
    private Inventory inventory;

    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
        inventory = FindObjectOfType<Inventory>();
    }

    public void AddQuest(Quest quest)
    {
        ActiveQuests.Add(quest);
        UpdateQuestText();
    }

    public void CheckQuestCompletion()
    {
        foreach (var quest in ActiveQuests)
        {
            if (quest.CheckCompletionCondition(playerController))
            {
                quest.IsCompleted = true;
                quest.GetComponent<InteractionObject>().dialogueLines[0] = "Thank you for collecting for me, here is a key shard for your troubles";
                UpdateQuestText();
            }
        }
    }

    public void UpdateKeyShards()
    {
        int keyShards = inventory.CountItem("KeyShards");
        keyshardsText.text = $"{keyShards}/5 Key Shards Collected";
    }

    public void UpdateQuestText()
    {
        if (ActiveQuests.All(quest => quest.IsCompleted))
        {
            questText.gameObject.SetActive(false);
        }
        else
        {
            questText.gameObject.SetActive(true);
            string questTextString = "";
            foreach (var quest in ActiveQuests)
            {
                if (!quest.IsCompleted)
                {
                    questTextString += $"Current Quest: {quest.name}\n";
                }
            }
            questText.text = questTextString;
        }
    }
}
