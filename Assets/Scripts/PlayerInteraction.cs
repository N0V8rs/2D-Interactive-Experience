using UnityEngine;

public class PlyerInteractions : MonoBehaviour
{
    public GameObject currentInterObj = null;
    public InteractionObject currentInterObjScript = null;
    public DialogueManager dialogueManager; // Add this line

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && currentInterObj)
        {
            if (currentInterObjScript.info)
            {
                currentInterObjScript.Info();
            }

            if (currentInterObjScript.pickup)
            {
                currentInterObjScript.Pickup();
            }

            if (currentInterObjScript.dialogue)
            {
                dialogueManager.StartDialogue(currentInterObjScript.dialogueText);
            }
        }

        if (dialogueManager.dialogueBox.activeInHierarchy && Input.GetKeyDown(KeyCode.Space))
        {
            dialogueManager.DisplayNextLine();
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Interactable") == true)
        {
            currentInterObj = other.gameObject;
            currentInterObjScript = currentInterObj.GetComponent<InteractionObject>();
            Debug.Log(other.name);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Interactable") == true)
        {
            currentInterObj = null;
            Debug.Log("Player exited");
        }
    }
}