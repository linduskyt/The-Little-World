using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DyradScript : MonoBehaviour
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
    public Sprite farmer1;
    public Sprite farmer2;
    public Sprite farmer3;
    public Sprite librarian1;
    public Sprite librarian2;
    public Sprite librarian3;
    public Sprite warrior1;
    public Sprite warrior2;
    public Sprite warrior3;
    public Sprite blackSmith1;
    public Sprite blackSmith2;
    public Sprite blackSmith3;
    public Sprite mage1;
    public Sprite mage2;
    public Sprite mage3;


    public int dialogueNum = 0; //0 for no dialog, 1 for message 1, 2 for message2, etc.

    void Start() { 
      theSpriteRenderer = dyradDialogue.GetComponent<SpriteRenderer>();
      //shopLoader = dyradShop.GetComponent<>();
      currentSprite = null;
      screenOne = Resources.Load<Sprite>("Dialogue1");
      screenTwo = Resources.Load<Sprite>("Dialogue2");
      screenThree = Resources.Load<Sprite>("Dialogue3");
      farmer1 = Resources.Load<Sprite>("dialogue4");
      farmer2 = Resources.Load<Sprite>("dialogue5");
      farmer3 = Resources.Load<Sprite>("dialogue6");
      librarian1 = Resources.Load<Sprite>("Librarian1");
      librarian2 = Resources.Load<Sprite>("Librarian2");
      librarian3 = Resources.Load<Sprite>("Librarian3");
      warrior1 = Resources.Load<Sprite>("Warrior1");
      warrior2 = Resources.Load<Sprite>("Warrior2");
      warrior3 = Resources.Load<Sprite>("Warrior3");
      blackSmith1 = Resources.Load<Sprite>("BlackSmith1");
      blackSmith2 = Resources.Load<Sprite>("BlackSmith2");
      blackSmith3 = Resources.Load<Sprite>("BlackSmith3");
      mage1 = Resources.Load<Sprite>("Mage1");
      mage2 = Resources.Load<Sprite>("Mage2");
      mage3 = Resources.Load<Sprite>("Mage3");
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
        else if (this.dialogueNum == 2)
        {
            theSpriteRenderer.sprite = null;
            dialogueNum++;
            npcShopHandler();
        }
        else if (this.dialogueNum == 3)
        {
            dyradShop.SetActive(false);
            theSpriteRenderer.sprite = screenThree;
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
