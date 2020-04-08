using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkTouchInput : MonoBehaviour
{
    public float speed = 0.1F;
    public float horizontalTranslation;
    public float verticalTranslation;
    //public Joystick joystick;
    public Animator animator;
    
    private Rigidbody2D myBody;

    // Start is called before the first frame update
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //horizontalTranslation = joystick.Horizontal * speed;
        //animator.SetFloat("HorizSpeed", horizTranslation);

        //verticalTranslation = joystick.Vertical * speed;
        //animator.SetFloat("VertSpeed", vertTranslation);

        //animator.SetBool("HorizGreaterThan",
        //        Mathf.Abs(horizTranslation) > Mathf.Abs(vertTranslation));
    }

    /*
        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Moved)
        {
            float horizTranslation = touch.position.x * speed;
            float vertTranslation = touch.position.x * speed;

            myBody.velocity = new Vector2(horizTranslation, vertTranslation);
            transform.Translate(horizTranslation, vertTranslation, 0);

            animator.SetFloat("VertSpeed", vertTranslation);
            animator.SetFloat("HorizSpeed", horizTranslation);
            animator.SetBool("HorizGreaterThan",
                Mathf.Abs(horizTranslation) > Mathf.Abs(vertTranslation));
        }
    }
    */
    private void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("OnCollisionEnter2D");
    }

    private void OnCollisionExit2D(Collision2D collision)
    {

    }
}



