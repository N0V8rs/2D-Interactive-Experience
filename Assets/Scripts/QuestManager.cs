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

    public void AddQuest(Quest quest)
    {
        ActiveQuests.Add(quest);
        UpdateQuestText();
    }

    public void CheckQuestCompletion()
    {
        foreach (var quest in ActiveQuests)
        {
            if (quest.CheckCompletionCondition(FindObjectOfType<PlayerController>()))
            {
                quest.IsCompleted = true;
                quest.GetComponent<InteractionObject>().dialogueLines[0] = "Thank you for collecting for me, here is a key shard for your troubles";
                UpdateQuestText();
            }
        }
    }

    public void UpdateKeyShards()
    {
        int keyShards = FindObjectOfType<Inventory>().KeyShards();
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
            questText.text = "";
            foreach (var quest in ActiveQuests)
            {
                if (!quest.IsCompleted)
                {
                    questText.text += "Current Quest: " + quest.name + "\n";
                }
            }
        }
    }
}
