using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
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
            transform.Translate(Vector3.forward * Time.deltaTime);
        }
        if (right)
        {
            // Move the object to the right along its x axis 2 unit/second.
            transform.Translate(Vector3.right * Time.deltaTime * 2);
        }
        if (left)
        {
            // Move the object to the left along its x axis 2 unit/second.
            transform.Translate(Vector3.left * Time.deltaTime * 2);
        }

        Debug.Log(transform.position);
    }
}
