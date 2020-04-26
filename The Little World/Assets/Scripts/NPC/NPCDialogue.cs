﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class NPCDialogue : MonoBehaviour
{

    public GameObject dyradDialogue;
    public GameObject dyradShop;
    public SpriteRenderer theSpriteRenderer;
    public Sprite currentSprite;
    public Sprite screenOne;
    public Sprite screenTwo;
    public int dialogueNum = 0; //0 for no dialog, 1 for message 1, 2 for message2, etc.

    void Start() { 
      dyradDialogue = GameObject.Find("dyradDialogue");
      dyradShop = GameObject.Find("NPC_Shop");
      theSpriteRenderer = dyradDialogue.GetComponent<SpriteRenderer>();
      //shopLoader = dyradShop.GetComponent<>();
      currentSprite = null;
      screenOne = Resources.Load<Sprite>("Dialogue1");
      screenTwo = Resources.Load<Sprite>("Dialogue2");
      dyradShop.SetActive(false);
    }
    //FindObjectOfType<DialogueManager>().StartDialogue(dialogue);

    private void OnMouseUpAsButton()
    {
        Debug.Log("dyrad clicked");
        handleDialogue();
    }

    private void handleDialogue()
    {
        if (this.dialogueNum == 0)
        {
            dyradShop.SetActive(false);
            theSpriteRenderer.sprite = screenOne;
            dialogueNum++;
        }
        else if (this.dialogueNum == 1)
        {
            theSpriteRenderer.sprite = screenTwo;
            dialogueNum++;
        }
        else if (this.dialogueNum >= 2)
        {
            theSpriteRenderer.sprite = null;
            dialogueNum = 0;
            npcShopHandler();
        }
        
    }

    private void npcShopHandler()
    {
        Debug.Log("Got Here");
        dyradShop.SetActive(true);
    }
}
