using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BSScript : MonoBehaviour
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
    public Sprite blackSmith1;
    public Sprite blackSmith2;
    public Sprite blackSmith3;


    public int dialogueNum = 0; //0 for no dialog, 1 for message 1, 2 for message2, etc.

     void Start()
    {
        BSDialogue = GameObject.Find("Canvas").transform.GetChild(3).GetChild(4).gameObject;

        theSpriteRenderer = BSDialogue.GetComponent<SpriteRenderer>();
        //shopLoader = dyradShop.GetComponent<>();
        currentSprite = null;
        blackSmith1 = Resources.Load<Sprite>("BlackSmith1");
        blackSmith2 = Resources.Load<Sprite>("BlackSmith2");
        blackSmith3 = Resources.Load<Sprite>("BlackSmith3");
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
            theSpriteRenderer.sprite = blackSmith1;
            dialogueNum++;
        }
        else if (this.dialogueNum == 1)
        {
            theSpriteRenderer.sprite = blackSmith2;
            dialogueNum++;
        }
        else if (this.dialogueNum == 2)
        {
            theSpriteRenderer.sprite = null;
            dialogueNum++;
            npcShopHandler();
        }
        else if (this.dialogueNum == 3)
        {
            dyradShop.SetActive(false);
            theSpriteRenderer.sprite = blackSmith3;
            dialogueNum++;
        }
        else if (this.dialogueNum >= 4)
        {

            theSpriteRenderer.sprite = null;
            dialogueNum = 0;

        }

    }

    private void npcShopHandler()
    {
        Debug.Log("Got Here");
        dyradShop.SetActive(true);
    }
}
