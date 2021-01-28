using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
    
{
    public float speed = 1.0f;
    public float HorizontalSpeed = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool up = Input.GetKey("w");
        bool left = Input.GetKey("a");
        bool right = Input.GetKey("d");

        if (up) 
        {
            // Move the object forward along its z axis 1 unit/second.
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
        if (right)
        {
            // Move the object to the right along its x axis 2 unit/second.
            transform.Translate(Vector3.right * Time.deltaTime * HorizontalSpeed);
        }
        if (left)
        {
            // Move the object to the left along its x axis 2 unit/second.
            transform.Translate(Vector3.left * Time.deltaTime * HorizontalSpeed);
        }

        Debug.Log(transform.position);
    }
}
