using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmerScript : MonoBehaviour
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
    public Sprite farmer1;
    public Sprite farmer2;
    public Sprite farmer3;


    public int dialogueNum = 0; //0 for no dialog, 1 for message 1, 2 for message2, etc.

    void Start()
    {
        farmerDialogue = GameObject.Find("Canvas").transform.GetChild(3).GetChild(2).gameObject;

        theSpriteRenderer = farmerDialogue.GetComponent<SpriteRenderer>();
        //shopLoader = dyradShop.GetComponent<>();
        currentSprite = null;
        farmer1 = Resources.Load<Sprite>("dialogue4");
        farmer2 = Resources.Load<Sprite>("dialogue5");
        farmer3 = Resources.Load<Sprite>("dialogue6");
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
            theSpriteRenderer.sprite = farmer1;
            dialogueNum++;
        }
        else if (this.dialogueNum == 1)
        {
            theSpriteRenderer.sprite = farmer2;
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
            theSpriteRenderer.sprite = farmer3;
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
