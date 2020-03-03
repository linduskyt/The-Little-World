using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkAnimated : MonoBehaviour
{

    public float speed = 10;
    public Animator animator;

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

        animator.SetFloat("VertSpeed", vertTranslation);
        animator.SetFloat("HorizSpeed", horizTranslation);
        animator.SetBool("HorizGreaterThan", 
            Mathf.Abs(horizTranslation) > Mathf.Abs(vertTranslation));
    }
}
