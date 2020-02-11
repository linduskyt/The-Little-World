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
        //Accept directional input and apply speed modifier
        float horizTranslation = Input.GetAxis("Horizontal") * speed;
        float vertTranslation = Input.GetAxis("Vertical") * speed;

        //Make movment relative to time rather than frames
        horizTranslation *= Time.deltaTime;
        vertTranslation *= Time.deltaTime;

        //Apply movment
        transform.Translate(horizTranslation, vertTranslation, 0);



    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("OnCollisionEnter2D");
    }
}
