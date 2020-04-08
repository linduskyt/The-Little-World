using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joystick : MonoBehaviour
{
    public Transform cameraTransform;
    public Animator animator;
    public float speed = 5.0f;
    private bool touchStart = false;
    private Vector2 pointA;
    private Vector2 pointB;

    public Transform circle;
    public Transform outerCircle;

    public Rigidbody2D myBody;
    public Rigidbody2D camera;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            pointA = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, 
                Input.mousePosition.y, Camera.main.transform.position.z));

            circle.transform.position = pointA;
            outerCircle.transform.position = pointA;
            circle.GetComponent<SpriteRenderer>().enabled = true;
            outerCircle.GetComponent<SpriteRenderer>().enabled = true;
        }
        if (Input.GetMouseButton(0))
        {
            touchStart = true;
            pointB = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, 
                Input.mousePosition.y, Camera.main.transform.position.z));
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
            moveCharacter(offset);
        }
        else
        {
            transform.Translate(0, 0, 0);
            cameraTransform.Translate(0, 0, 0);

            circle.GetComponent<SpriteRenderer>().enabled = false;
            outerCircle.GetComponent<SpriteRenderer>().enabled = false;

            animator.SetFloat("VertSpeed", 0);
            animator.SetFloat("HorizSpeed", 0);
            animator.SetBool("HorizGreaterThan", false);
        }

    }
    void moveCharacter(Vector2 direction)
    {
        float horizTranslation = direction.x * speed * Time.deltaTime;
        float vertTranslation = direction.y * speed * Time.deltaTime;

        myBody.velocity = new Vector2(horizTranslation, vertTranslation);
        transform.Translate(horizTranslation, vertTranslation, 0);

        animator.SetFloat("VertSpeed", vertTranslation);
        animator.SetFloat("HorizSpeed", horizTranslation);
        animator.SetBool("HorizGreaterThan", Mathf.Abs(horizTranslation) > 
            Mathf.Abs(vertTranslation));

        camera.velocity = new Vector2(horizTranslation, vertTranslation);
        cameraTransform.Translate(horizTranslation, vertTranslation, 0);

        circle.Translate(horizTranslation, vertTranslation, 0);
        outerCircle.Translate(horizTranslation, vertTranslation, 0);
    }
}