using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Current object the character is colliding with")]
    public GameObject currentInterObj = null;
    public InteractionObject currentObjScript = null;

    [Header("Player interact prompt")]
    public TMP_Text interactMessage;

    void Update()
    {
        DialogueManager dialogueManager = FindObjectOfType<DialogueManager>();
        if (dialogueManager.dialogueUI.activeInHierarchy && Input.GetKeyDown(KeyCode.Space))
        {
            dialogueManager.DisplayNextSentence();
        }
        else if (Input.GetKeyDown(KeyCode.Space) && currentInterObj == true)
        {
            if (currentObjScript.info == true)
            {
                currentObjScript.Info();
            }

            if (currentObjScript.pickup == true)
            {
                currentObjScript.Pickup();
            }

            if (currentObjScript.talking == true)
            {
                currentObjScript.Dialogue();
            }
        }

    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("InteractObject") == true)
        {
            currentInterObj = other.gameObject;
            currentObjScript = currentInterObj.GetComponent<InteractionObject>();
            interactMessage.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("InteractObject") == true)
        {
            currentInterObj = null;
            interactMessage.gameObject.SetActive(false);
        }
    }
}