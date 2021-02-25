using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingRock : MonoBehaviour
{
    Vector3 startPosition;
    Rigidbody body;
    Vector3 startVelocity;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        startPosition = transform.position;
        startVelocity = body.velocity;
    }

    // Update is called once per frame
    void Update()
    {
        // If the mass has been changed by the trigger field, then reset the position and velocity of the falling rock
        if (body.mass <= 4)
        {
            transform.position = startPosition;
            body.mass = 5;
            body.velocity = startVelocity;
        }
    }
}
