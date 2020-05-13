using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGirlScript : MonoBehaviour
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
    public Sprite warrior1;
    public Sprite warrior2;
    public Sprite warrior3;


    public int dialogueNum = 0; //0 for no dialog, 1 for message 1, 2 for message2, etc.

    void Start()
    {
        dungeonDialogue = GameObject.Find("Canvas").transform.GetChild(3).GetChild(3).gameObject;

        theSpriteRenderer = dungeonDialogue.GetComponent<SpriteRenderer>();
        //shopLoader = dyradShop.GetComponent<>();
        currentSprite = null;
        warrior1 = Resources.Load<Sprite>("Warrior1");
        warrior2 = Resources.Load<Sprite>("Warrior2");
        warrior3 = Resources.Load<Sprite>("Warrior3");
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
            theSpriteRenderer.sprite = warrior1;
            dialogueNum++;
        }
        else if (this.dialogueNum == 1)
        {
            theSpriteRenderer.sprite = warrior2;
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
            theSpriteRenderer.sprite = warrior3;
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
