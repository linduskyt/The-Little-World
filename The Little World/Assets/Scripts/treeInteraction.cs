using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treeInteraction : MonoBehaviour
{
    [SerializeField] private int treeMaxHealth = 1;
    [SerializeField] private int treeHealth = -1;
    [SerializeField] private Player player = null;
    [SerializeField] private ItemObject item = null;

    private DisplayHotbar hotbar = null;

    private GameObject tree = null;

    private int numItem = -1;

    private bool inRange = false;

    private void Start()
    {
        hotbar = GameObject.FindWithTag("Hotbar").GetComponent<DisplayHotbar>();
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        treeHealth = Random.Range(3, 7);
        treeMaxHealth = treeHealth;
        float fNumItem = treeHealth * Random.Range(1.5f, 3f);
        numItem = (int)fNumItem;
        Debug.Log("fNum:" + fNumItem + "\nnum: " + numItem);
    }

    private void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
        if (hit)
            tree = hit.transform.gameObject;
        if (treeHealth <= 0)
        {
            Destroy(tree);
            player.inventory.AddItem(item.CreateItem(), numItem);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            inRange = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            inRange = false;
    }

    private void OnMouseOver()
    {
        if(player && hotbar.selectedSlot.item.type == ItemType.Equipment && hotbar.selectedSlot.item.Id == 3 && inRange)
        {
            if (player.mode == 1 && Input.GetMouseButtonUp(0))
            {
                treeHealth = treeHealth - hotbar.selectedSlot.item.buffs[0].value;

                if (treeHealth < 0)
                    treeHealth = 0;

                Debug.Log("Tree Health: " + treeHealth + "\nTree Max Health: " + treeMaxHealth);

                Debug.Log("Tool Mining Speed: " + hotbar.selectedSlot.item.buffs[0].value);

                float floatScale = 0;
                for (int i = 0; treeHealth > 0 && i < hotbar.selectedSlot.item.buffs[0].value; i++)
                {
                    floatScale = 3f - (((2f / (treeMaxHealth)) * (treeMaxHealth - treeHealth)));
                }

                Debug.Log("Float: " + floatScale);

                if(!tree.transform.GetChild(3).gameObject.activeInHierarchy)
                {
                    tree.transform.GetChild(2).gameObject.SetActive(true);
                    tree.transform.GetChild(3).gameObject.SetActive(true);
                    tree.transform.GetChild(4).gameObject.SetActive(true);
                }

                tree.transform.GetChild(3).GetComponentInChildren<Transform>().localScale = new Vector3(floatScale, 0.1f, 1.0f);
            }
        }
    }
}