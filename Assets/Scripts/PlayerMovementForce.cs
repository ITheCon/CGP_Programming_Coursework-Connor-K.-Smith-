using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementForce : MonoBehaviour
{
    public float speed = 1.0f;
    public float HorizontalSpeed = 2.0f;
    public Rigidbody rb;

    int jumpCooldown = 0;
    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        bool up = Input.GetKey("w");
        bool left = Input.GetKey("a");
        bool right = Input.GetKey("d");
        bool down = Input.GetKey("s");
        bool jump = Input.GetKey("space");

        if (up)
        {
            // Move the object forward along its z axis with force
            rb.AddForce(transform.forward * (speed + speed*0.5f*rb.mass));
        }
        if (right)
        {
            // Move the object to the right along its x axis with force.
            rb.AddForce(transform.right * (speed + speed * 0.7f * rb.mass));
        }
        else if (left)
        {
            // Move the object to the left along its -x axis with force.
            rb.AddForce(-transform.right * (speed + speed * 0.7f * rb.mass));
        }
        else if (down)
        {
            // Move the object forward along its -z axis with force
            rb.AddForce(-transform.forward * (speed + speed * 0.5f * rb.mass));
        }
        else if (jump && jumpCooldown <= 0)
        {
            // Move the object forward along its y axis with force
            rb.AddForce(transform.up * ((speed*50f) + (speed*50f*rb.mass)));
            jumpCooldown = 60;
        }
        jumpCooldown -= 1;

    }


}
