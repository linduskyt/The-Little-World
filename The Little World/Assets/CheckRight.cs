using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckRight : MonoBehaviour
{
    public Transform locations;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        
        locations.position = new Vector2(0, Random.Range(-10, 10));
        GameObject Character =  GameObject.Find("frienTileSet_0");

        

        Character.transform.position = Vector2.MoveTowards(transform.position, locations.position, 0.5f * Time.deltaTime);
        Debug.Log("Called");
    }
}
