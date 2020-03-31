using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceBlock : MonoBehaviour
{

    public GameObject blockPreFab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //If right click, drop block under mouse
        if (Input.GetMouseButtonUp(1) == true)
        {
            Debug.Log("Upclick");
            Vector3 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

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
        //If left click, break block under mouse
        else if (Input.GetMouseButtonUp(0) == true)
        {

        }
    }
}
