using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlaceBlockWithInventory : MonoBehaviour
{
    [SerializeField] GameObject blockPreFab = null;
    private Player player;
    private DisplayHotbar hotbar;
    private Item item;
    // Start is called before the first frame update
    void Start()
    {
        hotbar = GameObject.FindWithTag("Hotbar").GetComponent<DisplayHotbar>();
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.mode == 1)
        {
            item = hotbar.selectedSlot.item;
            if (item.type == ItemType.Block)
            {
                Debug.Log("Placable: " + hotbar.myInventory.isPlacable);
                //If right click, drop block under mouse
                Vector3 mousePos = Input.mousePosition;
                mousePos = Camera.main.ScreenToWorldPoint(mousePos);
                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

                RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
                //if (hit.collider != null) { Debug.Log("Placable: False"); }
                //if (hit.collider == null) { Debug.Log("Placable: True"); }
                //Debug.Log("Item Placable Test 1: " + hotbar.myInventory.isPlacable);
                //Debug.Log("Item Placable Test 2: " + !hotbar.myInventory.isDragging);
                if (Input.GetMouseButtonUp(1) == true && hotbar.selectedSlot.amount > 0 && hit.collider == null && !(hotbar.myInventory.isDragging) && hotbar.myInventory.isPlacable)
                {
                    Debug.Log("Upclick");
                    Debug.Log("Removing one " + item.Name);
                    hotbar.selectedSlot.addAmount(-1);

                    if (mousePos.x > 0.16F)
                        mousePos.x += 0.16F;
                    else if (mousePos.x < -0.16F)
                        mousePos.x -= 0.16F;

                    if (mousePos.y > 0.16F)
                        mousePos.y += 0.16F;
                    else if (mousePos.y < -0.16F)
                        mousePos.y -= 0.16F;

                    mousePos.x = mousePos.x - (mousePos.x % 0.32F);
                    mousePos.y = mousePos.y - (mousePos.y % 0.32F);
                    //mousePos.z = -5 + (mousePos.y * .0001F);
                    mousePos.z = 0;

                    Instantiate(blockPreFab, mousePos, Quaternion.identity);

                }

                if (hotbar.selectedSlot.amount <= 0)
                {
                    hotbar.selectedSlot.ID = -1;
                }
            }
            //If left click, break block under mouse
            if (Input.GetMouseButtonUp(0) == true)
            {
                //Debug.Log("Left Click");
                Vector3 mousePos = Input.mousePosition;
                mousePos = Camera.main.ScreenToWorldPoint(mousePos);
                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

                //Detects if there is an object where the mouse is at.
                RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
                if (hit.collider != null && hit.collider.CompareTag("ItemBlock"))
                {
                    //Debug.Log("Destroying Object");
                    GroundBlock destroyBlock = hit.collider.gameObject.GetComponent<GroundBlock>();
                    for (int i = 0; i < hotbar.myInventory.inventory.Container.Items.Length; i++)
                    {
                        if (hotbar.myInventory.inventory.Container.Items[i].item.Id == destroyBlock.item.Id)
                        {
                            Debug.Log("Adding Item");
                            //Adds an item to the slot containing the same item id if it exists.
                            hotbar.myInventory.inventory.AddItem(destroyBlock.item.CreateItem(), 1);
                            break;
                        }

                        //If item doesn't exist in the inventory, add a new item of the destroyed block to the inventory.
                        if (i + 1 >= hotbar.myInventory.inventory.Container.Items.Length)
                        {
                            Debug.Log("Adding NEW Item");
                            hotbar.myInventory.inventory.AddItem(new Item(destroyBlock.item), 1);
                        }
                    }
                    Destroy(hit.collider.gameObject);
                }
            }
        }
    }
}
