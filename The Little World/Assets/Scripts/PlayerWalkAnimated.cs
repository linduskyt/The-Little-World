using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkAnimated : MonoBehaviour
{
    private const int V = 130;
    public float speed = 0.1F;
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
        float horizTranslation = Input.GetAxis("Horizontal") * speed;
        float vertTranslation = Input.GetAxis("Vertical") * speed;

        myBody.velocity = new Vector2(horizTranslation, vertTranslation);
        transform.Translate(horizTranslation / V, vertTranslation / V, 0);

        animator.SetFloat("VertSpeed", vertTranslation);
        animator.SetFloat("HorizSpeed", horizTranslation);
        animator.SetBool("HorizGreaterThan", 
            Mathf.Abs(horizTranslation) > Mathf.Abs(vertTranslation));
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("OnCollisionEnter2D");
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        
    }
}
