using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class NPCGraphics : MonoBehaviour
{
    public AIPath aiPath;
    public Animator animator;
    private bool Up = false;
    private bool Down = false;
    private bool Right = false;
    private bool Left = false;
    // Update is called once per frame
    void Update()
    {
        if (aiPath.desiredVelocity.x >= 0.01f)
        {
            //transform.localScale = new Vector3(-1f, 1f, 1f);
            animator.SetBool("walkDown", false);
            animator.SetBool("walkRight", false);
            animator.SetBool("walkLeft", true);
            animator.SetBool("walkUp", false);
            //animator.SetBool("Idle", true);

            Up = false;
            Down = false;
            Left = true;
            Right = false;
        }
        if (aiPath.desiredVelocity.x < -0.01f)
        {
            // transform.localScale = new Vector3(1f, 1f, 1f);

            animator.SetBool("walkDown", false);
            animator.SetBool("walkRight", false);
            animator.SetBool("walkLeft", false);
            animator.SetBool("walkUp", false);
            //animator.SetBool("Idle", true);

            Up = false;
            Down = false;
            Left = false;
            Right = true;
        }
        if (aiPath.desiredVelocity.y > 0.01f)
        {
            //transform.localScale = new Vector3(1f, -1f, 1f);

            animator.SetBool("walkDown", false);
            animator.SetBool("walkRight", false);
            animator.SetBool("walkLeft", false);
            animator.SetBool("walkUp", true);
            //animator.SetBool("Idle", true);

            Up = true;
            Down = false;
            Left = false;
            Right = false;
        }
        if (aiPath.desiredVelocity.y < -0.01f)
        {
            //transform.localScale = new Vector3(-1f, -1f, 1f);

            animator.SetBool("walkDown", true);
            animator.SetBool("walkRight", false);
            animator.SetBool("walkLeft", false);
            animator.SetBool("walkUp", false);
            //animator.SetBool("Idle", true);

            Up = false;
            Down = true;
            Left = false;
            Right = false;
        }

    }
}
