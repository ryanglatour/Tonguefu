using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;
    public Animator animator;
    private Queue<string> sentences;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        StartInitialDialogue();
    }

    // This method finds the DialogueTrigger object and starts its dialogue
    // This method finds the DialogueTrigger object and starts its dialogue
    void StartInitialDialogue()
    {
        StartCoroutine(StartDialogueWithDelay(1.0f)); // Change the initial delay time here
    }

    IEnumerator StartDialogueWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for specified initial delay
        DialogueTrigger dialogueTrigger = FindObjectOfType<DialogueTrigger>();
        if (dialogueTrigger != null)
        {
            dialogueTrigger.TriggerDialogue();
        }
    }


    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("IsOpen", true);
        nameText.text = dialogue.characterName;
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

public void DisplayNextSentence()
{
    if (sentences.Count == 0)
    {
        StartInitialDialogue();
        DialogueTrigger dialogueTrigger = FindObjectOfType<DialogueTrigger>();
        if(dialogueTrigger.dialogueIndex == 2){
            EndDialogue();
        }
        return;
    }
    string sentence = sentences.Dequeue();
    StopAllCoroutines();
    StartCoroutine(TypeSentenceWithDelay(sentence, 2.0f)); // Change the delay time here
}

IEnumerator TypeSentenceWithDelay(string sentence, float delay)
{
    dialogueText.text = "";
    foreach (char letter in sentence.ToCharArray())
    {
        dialogueText.text += letter;
        yield return null;
    }
    yield return new WaitForSeconds(delay); // Wait for specified delay
    DisplayNextSentence(); // Display the next sentence after the delay
}

    public void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
    }
}
