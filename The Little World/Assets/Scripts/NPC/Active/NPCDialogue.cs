using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class NPCDialogue : MonoBehaviour
{
    public Dialogue dialogue;

    FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
}
