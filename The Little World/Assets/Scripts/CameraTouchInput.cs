using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTouchInput : MonoBehaviour
{
    public float speed = 0.1F;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                float horizTranslation = touch.position.x * speed;
                float vertTranslation = touch.position.x * speed;
                
                transform.Translate(horizTranslation, vertTranslation, 0);
            }
        }
    }
}
