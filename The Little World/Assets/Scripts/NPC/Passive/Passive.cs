using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passive : MonoBehaviour
{
    public float speed;
    private float waitTime;
    public float startWaitTime;

    public Transform locations;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;



    private void Start()
    {
        waitTime = startWaitTime;

        locations.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, locations.position, speed*Time.deltaTime);

        if (Vector2.Distance(transform.position, locations.position) < 0.2f)
        {
            if (waitTime <= 0)
            {
                locations.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
                waitTime = startWaitTime;
            }

            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }
}
