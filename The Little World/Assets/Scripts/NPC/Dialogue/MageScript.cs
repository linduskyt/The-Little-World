using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MageScript : MonoBehaviour
{
    [SerializeField] private GameObject myCanvas;

    public GameObject dyradDialogue;
    public GameObject farmerDialogue;
    public GameObject librianDialogue;
    public GameObject dungeonDialogue;
    public GameObject BSDialogue;
    public GameObject mageDialogue;

    public GameObject dyradShop;
    public SpriteRenderer theSpriteRenderer;
    public Sprite currentSprite;
    public Sprite mage1;
    public Sprite mage2;
    public Sprite mage3;


    public int dialogueNum = 0; //0 for no dialog, 1 for message 1, 2 for message2, etc.

    void Start()
    {
        mageDialogue = GameObject.Find("Canvas").transform.GetChild(3).GetChild(0).gameObject;


       theSpriteRenderer = mageDialogue.GetComponent<SpriteRenderer>();

        //shopLoader = dyradShop.GetComponent<>();
        currentSprite = null;
        mage1 = Resources.Load<Sprite>("Mage1");
        mage2 = Resources.Load<Sprite>("Mage2");
        mage3 = Resources.Load<Sprite>("Mage3");
        dyradShop.SetActive(false);
    }
        //FindObjectOfType<DialogueManager>().StartDialogue(dialogue);

    private void OnMouseUpAsButton()
    {
        Debug.Log("mage clicked");
        handleDialogue();
    }

    private void handleDialogue()
    {
        if (this.dialogueNum == 0)
        {
            dyradShop.SetActive(false);
            theSpriteRenderer.sprite = mage1;
            dialogueNum++;
        }
        else if (this.dialogueNum == 1)
        {
            theSpriteRenderer.sprite = mage2;
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
            theSpriteRenderer.sprite = mage3;
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
