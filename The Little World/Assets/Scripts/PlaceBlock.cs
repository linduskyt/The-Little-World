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
            mousePos.x = ((mousePos.x -400)/ 80) + transform.position.x;
            
            if (mousePos.x < 0.3F && mousePos.x > -0.3F)
            { }
            else if (mousePos.x > 0)
                mousePos.x += 0.3F;
            else
                mousePos.x -= 0.3F;
            mousePos.x = mousePos.x - (mousePos.x % (0.6F));


            mousePos.y = ((mousePos.y - 640) / 80) + transform.position.y;
            
            if (mousePos.y < 0.3F && mousePos.y > -0.3F)
            { }
            else if (mousePos.y > 0)
                mousePos.y += 0.3F;
            else
                mousePos.y -= 0.3F;
            mousePos.y = mousePos.y - (mousePos.y % (0.6F));
            mousePos.z = 0;
            Instantiate(blockPreFab, mousePos, Quaternion.identity);
        }
    }
}
