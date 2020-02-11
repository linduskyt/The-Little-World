using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalking : MonoBehaviour
{

    public float speed = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizTranslation = Input.GetAxis("Horizontal") * speed;
        float vertTranslation = Input.GetAxis("Vertical") * speed;

        horizTranslation *= Time.deltaTime;
        vertTranslation *= Time.deltaTime;

        transform.Translate(horizTranslation, vertTranslation, 0);
    }
}
