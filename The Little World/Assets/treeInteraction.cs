using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treeInteraction : MonoBehaviour
{
    [SerializeField] private Player player;

    [SerializeField] private int treeHealth;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        treeHealth = 3;
    }

    private void Update()
    {
        if (treeHealth <= 0)
        {
            Destroy(this);
        }
    }

    private void OnMouseOver()
    {
        if(player)
        {
            if (player.mode == 1 && Input.GetMouseButtonUp(1))
            {
                treeHealth -= 1;
                Debug.Log("Tree Health: " + treeHealth);
            }
        }
    }
}
