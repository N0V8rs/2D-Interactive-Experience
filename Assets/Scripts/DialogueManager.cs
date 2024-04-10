using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public PlayerController playerController;
    public GameObject dialogueBox;
    public TMP_Text dialogueText;
    public GameObject player;
    private string[] dialogueLines;
    private int currentLine = 0;

    private bool canDisplayNextLine = false;

    public void StartDialogue(string[] lines)
    {
        dialogueLines = lines;
        currentLine = 0;
        dialogueText.text = dialogueLines[currentLine];
        dialogueBox.SetActive(true);
        player.GetComponent<PlayerController>().enabled = false;
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        player.GetComponentInChildren<Animator>().enabled = false;
        StartCoroutine(EnableNextLineAfterDelay(1f));
    }

    public void DisplayNextLine()
    {
        if (!canDisplayNextLine) 
            return; 

        currentLine++;
        if (currentLine < dialogueLines.Length)
        {
            dialogueText.text = dialogueLines[currentLine];
        }
        else
        {
            dialogueBox.SetActive(false);
            player.GetComponent<PlayerController>().enabled = true;
            player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            player.GetComponentInChildren<Animator>().enabled = true;
            canDisplayNextLine = false; 
        }
    }

    // Add this method
    private IEnumerator EnableNextLineAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        canDisplayNextLine = true;
    }
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerController = player.GetComponent<PlayerController>();
            dialogueBox = player.transform.Find("DialogueText").gameObject;
            dialogueText = dialogueBox.GetComponentInChildren<TMP_Text>();
        }
        else
        {
            Debug.LogError("Player not found");
        }
    }
}
