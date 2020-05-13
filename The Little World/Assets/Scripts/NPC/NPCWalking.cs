using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCWalking : MonoBehaviour
{

    public float speed = 40F;
    public Animator animator;
    private Rigidbody2D myBody;
    private short timer = 0;
    private float horizTranslation;
    private float vertTranslation;

    // Start is called before the first frame update
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer == 0) { 
        //Accept directional input and apply speed modifier
        horizTranslation = Random.Range(0, 500) * speed / 500;
        vertTranslation = Random.Range(0, 500) * speed / 500;
            timer = 200;
        }

        myBody.velocity = new Vector2(horizTranslation, vertTranslation);
        animator.SetFloat("VertSpeed", vertTranslation);
        animator.SetFloat("HorizSpeed", horizTranslation);
        animator.SetBool("HorizGreaterThan",
            Mathf.Abs(horizTranslation) > Mathf.Abs(vertTranslation));

        --timer;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        //Debug.Log("OnCollisionEnter2D");
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
    
    }
}
