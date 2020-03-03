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
        if (Input.GetMouseButtonUp(1) == true)
        {
            Debug.Log("Upclick");
            Vector3 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            if (mousePos.x > 0.32F)
                mousePos.x += 0.32F;
            else if (mousePos.x < -0.32F)
                mousePos.x -= 0.32F;

            if (mousePos.y > 0.32F)
                mousePos.y += 0.32F;
            else if (mousePos.y < -0.32F)
                mousePos.y -= 0.32F;

            mousePos.x = mousePos.x - (mousePos.x % 0.64F);
            mousePos.y = mousePos.y - (mousePos.y % 0.64F);
            mousePos.z = -5;

            
            Instantiate(blockPreFab, mousePos, Quaternion.identity);
            
        }
    }
}
