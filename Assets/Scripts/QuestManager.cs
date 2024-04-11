using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public List<Quest> ActiveQuests;
    public TMP_Text questText;
    public TMP_Text stoneCountText;

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
                quest.GetComponent<InteractionObject>().dialogueLines[0] = "You have completed the quest! Here is a stone to help you on your journey!";
                UpdateQuestText();
            }
        }
    }

    public void UpdateStoneCountText()
    {
        int stoneCount = FindObjectOfType<Inventory>().CountStones();
        stoneCountText.text = $"{stoneCount}/5 Stones Collected";
    }

    public void UpdateQuestText()
    {
        if (ActiveQuests.All(quest => quest.IsCompleted))
        {
            // If all quests are completed, hide the quest text UI
            questText.gameObject.SetActive(false);
        }
        else
        {
            // If there are active quests, show the quest text UI and update the text
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
