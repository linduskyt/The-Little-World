using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class NPCDialogue : MonoBehaviour
{

    public GameObject dyradDialogue;
    public GameObject farmerDialogue;
    public GameObject librianDialogue;
    public GameObject dungeonDialogue;
    public GameObject BSDialogue;
    public GameObject mageDialogue;

    public GameObject dyradShop;
    public SpriteRenderer theSpriteRenderer;
    public Sprite currentSprite;
    public Sprite screenOne;
    public Sprite screenTwo;
    public Sprite screenThree;
    public int dialogueNum = 0; //0 for no dialog, 1 for message 1, 2 for message2, etc.

    void Start() { 
      dyradDialogue = GameObject.Find("dyradDialogue");
      farmerDialogue = GameObject.Find("farmerDialogue");
      librianDialogue = GameObject.Find("librianDialogue");
      dungeonDialogue = GameObject.Find("dungeonDialogue");
      BSDialogue = GameObject.Find("BSDialogue");
      mageDialogue = GameObject.Find("mageDialogue");

      dyradShop = GameObject.Find("NPC_Shop");
      theSpriteRenderer = dyradDialogue.GetComponent<SpriteRenderer>();
      //shopLoader = dyradShop.GetComponent<>();
      currentSprite = null;
      screenOne = Resources.Load<Sprite>("Dialogue1");
      screenTwo = Resources.Load<Sprite>("Dialogue2");
      screenThree = Resources.Load<Sprite>("Dialogue3");

        //dyradShop.SetActive(false);
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
        else if (this.dialogueNum == 2)
        {
            theSpriteRenderer.sprite = null;
            dialogueNum++;
            npcShopHandler();
        }
        else if (this.dialogueNum >= 3)
        {
            dyradShop.SetActive(false);
            theSpriteRenderer.sprite = screenThree;
            dialogueNum = 0;
 
        }

    }

    private void npcShopHandler()
    {
        Debug.Log("Got Here");
        dyradShop.SetActive(true);
    }
}
