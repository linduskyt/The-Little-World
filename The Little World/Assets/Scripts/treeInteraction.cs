﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treeInteraction : MonoBehaviour
{
    [SerializeField] private Player player = null;

    [SerializeField] private int treeHealth = -1;

    [SerializeField] private ItemObject item = null;

    private GameObject tree = null;

    private int numItem = -1;

    [SerializeField] private int treeMaxHealth = 1;

    private void Start()
    {
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

    private void OnMouseOver()
    {
        if(player)
        {
            if (player.mode == 1 && Input.GetMouseButtonUp(1))
            {
                treeHealth = treeHealth - 1;
                Debug.Log("Tree Health: " + treeHealth + "\nTree Max Health: " + treeMaxHealth);
                


                float floatScale = 3f - ((2f / (treeMaxHealth)) * (treeMaxHealth - treeHealth));
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
