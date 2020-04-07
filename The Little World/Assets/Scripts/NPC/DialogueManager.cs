using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    private Queue<string> sentences = new Queue<string>();

    public GameObject NPCOptions;

    public Text nameText;
    public Text dialogueText;

    // Start is called before the first frame update
    void Start()
    {
         
    }

    void Update()
    {

    }
    public void StartDialogue(Dialogue dialogue)
    {
       //Debug.Log("Starting conversation with " + dialogue.name);

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;

    }

    void EndDialogue()
    {
        Debug.Log("End of conversation");
    }

    public void nextMenu()
    {

    }

   
}
