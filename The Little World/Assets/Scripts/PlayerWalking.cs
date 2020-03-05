using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalking : MonoBehaviour
{
    private const int V = 130;
    public float speed = 0.1F;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizTranslation = Input.GetAxis("Horizontal") * speed;
        float vertTranslation = Input.GetAxis("Vertical") * speed;

        transform.Translate(horizTranslation / V, vertTranslation / V, 0);
    }
}
