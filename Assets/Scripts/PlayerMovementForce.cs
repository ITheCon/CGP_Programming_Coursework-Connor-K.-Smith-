using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementForce : MonoBehaviour
{
    public float speed = 1.0f;
    public float HorizontalSpeed = 2.0f;
    public Rigidbody rb;
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

        if (up)
        {
            // Move the object forward along its z axis with force
            rb.AddForce(transform.forward * (speed + speed*0.5f*rb.mass));
        }
        if (right)
        {
            // Move the object to the right along its x axis 2 unit/second.
            rb.AddForce(transform.right * (speed + speed * 0.7f * rb.mass));
        }
        else if (left)
        {
            // Move the object to the left along its x axis 2 unit/second.
            rb.AddForce(-transform.right * (speed + speed * 0.7f * rb.mass));
        }

    }
}
