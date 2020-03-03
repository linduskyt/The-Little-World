using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalking : MonoBehaviour
{

    public float speed = 0.1F;
    private Rigidbody2D myBody;

    // Start is called before the first frame update
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Accept directional input and apply speed modifier
        float horizTranslation = Input.GetAxis("Horizontal") * speed;
        float vertTranslation = Input.GetAxis("Vertical") * speed;

        myBody.velocity = new Vector2(horizTranslation, vertTranslation);



    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("OnCollisionEnter2D");
    }

    private void OnCollisionExit2D(Collision2D collision)
    {

    }
}
