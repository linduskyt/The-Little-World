using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LibrarianScript : MonoBehaviour
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
    public Sprite librarian1;
    public Sprite librarian2;
    public Sprite librarian3;
    


    public int dialogueNum = 0; //0 for no dialog, 1 for message 1, 2 for message2, etc.

    void Start()
    {
        librianDialogue = GameObject.Find("Canvas").transform.GetChild(3).GetChild(1).gameObject;

        theSpriteRenderer = librianDialogue.GetComponent<SpriteRenderer>();
        //shopLoader = dyradShop.GetComponent<>();
        currentSprite = null;
        librarian1 = Resources.Load<Sprite>("Librarian1");
        librarian2 = Resources.Load<Sprite>("Librarian2");
        librarian3 = Resources.Load<Sprite>("Librarian3");
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
            theSpriteRenderer.sprite = librarian1;
            dialogueNum++;
        }
        else if (this.dialogueNum == 1)
        {
            theSpriteRenderer.sprite = librarian2;
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
            theSpriteRenderer.sprite = librarian3;
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
