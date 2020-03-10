using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkTouchInput : MonoBehaviour
{
    public float speed = 0.1F;
    public Animator animator;
    
    private Rigidbody2D myBody;
    private Vector3 position;
    private float width;
    private float height;

    // Start is called before the first frame update
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        width = (float)Screen.width / 2.0f;
        height = (float)Screen.height / 2.0f;

        position = new Vector3(0.0f, 0.0f, 0.0f);
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

                myBody.velocity = new Vector2(horizTranslation, vertTranslation);
                transform.Translate(horizTranslation, vertTranslation, 0);

                animator.SetFloat("VertSpeed", vertTranslation);
                animator.SetFloat("HorizSpeed", horizTranslation);
                animator.SetBool("HorizGreaterThan",
                    Mathf.Abs(horizTranslation) > Mathf.Abs(vertTranslation));
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("OnCollisionEnter2D");
    }

    private void OnCollisionExit2D(Collision2D collision)
    {

    }
}

