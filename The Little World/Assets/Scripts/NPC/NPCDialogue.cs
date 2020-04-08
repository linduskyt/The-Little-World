using System.Collections;
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
      dyradShop = GameObject.Find("ShopImage");
      theSpriteRenderer = dyradDialogue.GetComponent<SpriteRenderer>();
      currentSprite = null;
      screenOne = Resources.Load<Sprite>("Dialogue1");
      screenTwo = Resources.Load<Sprite>("Dialogue2");
    
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
        }
    }
}
