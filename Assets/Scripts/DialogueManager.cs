using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [Header("References")]

    public GameObject dialogueUI;
    public TMP_Text dialogueText;
    public GameObject player;
    public Animator animator;

    private Queue<string> dialogueQueue;

    // For typing effect
    private bool isTyping = false;
    private string currentSentence;

    void Start()
    {
        dialogueQueue = new Queue<string>();
    }

    public void StartDialogue(string[] sentences)
    {
        // Clear the queue
        dialogueQueue.Clear();
        dialogueUI.SetActive(true);

        player.GetComponent<PlayerController>().enabled = false;


        // Set the animator speed to 0
        animator.enabled = false;
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        foreach (string currentLine in sentences)
        {
            dialogueQueue.Enqueue(currentLine);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (isTyping)
        {
            StopAllCoroutines();
            dialogueText.text = currentSentence;
            isTyping = false;
        }
        else if (dialogueQueue.Count == 0)
        {
            EndDialogue();
        }
        else
        {
            currentSentence = dialogueQueue.Dequeue();
            StopAllCoroutines(); // Stop any previous typing effects
            StartCoroutine(TypeSentence(currentSentence));
        }
    }

    void EndDialogue()
    {
        animator.enabled = true;
        dialogueQueue.Clear();
        dialogueUI.SetActive(false);

        // Enable player movement
        player.GetComponent<PlayerController>().enabled = true;
        player.GetComponent<PlayerInteraction>().enabled = true;
    }

    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.05f); // Wait for 0.05 seconds
        }
        isTyping = false;
    }
}
