using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue[] dialogues;

    public int dialogueIndex = 0;

    public void TriggerDialogue()
    {
        if (dialogueIndex < dialogues.Length)
        {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogues[dialogueIndex]);
            dialogueIndex++;
        }
        else
        {
            DialogueManager dialogueManager = FindObjectOfType<DialogueManager>();
            dialogueManager.EndDialogue();
        }
    }
}
