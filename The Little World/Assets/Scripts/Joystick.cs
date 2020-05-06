using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joystick : MonoBehaviour
{
    public Transform cameraTransform;
    public Animator animator;
    public float speed = 5.0f;
    private bool touchStart = false;

    private float cameraPositionX;
    private float cameraPositionY;

    private Vector2 pointIn;
    private Vector2 pointA;
    private Vector2 pointB;

    private Rect bottomLeft;

    public Transform joyTransform;
    public Transform outerJoyTransform;

    public Rigidbody2D playerRigidBody;
    public Rigidbody2D cameraRigidBody;

    public Rigidbody2D joyRigidBody;
    public Rigidbody2D outerJoyRigidBody;

    // Update is called once per frame
    void Update()
    {
        cameraPositionX = cameraTransform.position.x;
        cameraPositionY = cameraTransform.position.y;

        bottomLeft = new Rect(cameraPositionX - 8, cameraPositionY - 5, 4, 4);

        pointA = new Vector2(cameraPositionX - 6, cameraPositionY - 3);

        if (Input.GetMouseButtonDown(0))
        {
            pointIn = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, 
                Input.mousePosition.y, Camera.main.transform.position.z));

            //joyTransform.transform.position = pointA;
            //outerJoyTransform.transform.position = pointA;
        }
        if (Input.GetMouseButton(0))
        {
            if (bottomLeft.Contains(pointIn) || touchStart)
            {
                touchStart = true;
                pointB = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                    Input.mousePosition.y, Camera.main.transform.position.z));
            }
        }
        else
        {
            touchStart = false;
        }
    }

    private void FixedUpdate()
    {
        if (touchStart)
        {
            Vector2 offset = pointB - pointA;
            Vector2 direction = Vector2.ClampMagnitude(offset, 1.0f);
            moveCharacter(direction);

            joyTransform.transform.position = new Vector2(pointA.x + direction.x, pointA.y + direction.y);
            outerJoyTransform.transform.position = pointA;
        }
        else
        {
            playerRigidBody.velocity = new Vector2(0, 0);
            transform.Translate(0, 0, 0);
            cameraRigidBody.velocity = new Vector2(0, 0);
            cameraTransform.Translate(0, 0, 0);

            joyTransform.transform.position = pointA;
            outerJoyTransform.transform.position = pointA;

            animator.SetFloat("VertSpeed", 0);
            animator.SetFloat("HorizSpeed", 0);
            animator.SetBool("HorizGreaterThan", false);
        }
    }

    void moveCharacter(Vector2 direction)
    {
        float horizTranslation = direction.x * speed;
        float vertTranslation = direction.y * speed;

        playerRigidBody.velocity = new Vector2(horizTranslation, vertTranslation);
        //transform.Translate(horizTranslation, vertTranslation, 0);

        animator.SetFloat("VertSpeed", vertTranslation);
        animator.SetFloat("HorizSpeed", horizTranslation);
        animator.SetBool("HorizGreaterThan", Mathf.Abs(horizTranslation) > 
            Mathf.Abs(vertTranslation));

        cameraRigidBody.velocity = new Vector2(horizTranslation, vertTranslation);

        //joyRigidBody.velocity = new Vector2(horizTranslation, vertTranslation);
        //outerJoyRigidBody.velocity = new Vector2(horizTranslation, vertTranslation);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {

    }

    private void OnCollisionExit2D(Collision2D collision)
    {

    }
}