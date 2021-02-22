using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SnowballSnowCollect : MonoBehaviour
{
    float speed;
    float mass;
    Vector3 size;
    int delay = 120;
    GameObject collidingObject;
    // Start is called before the first frame update
    void Start()
    {
        mass = transform.parent.GetComponent<Rigidbody>().mass;
        size = GetComponent<Transform>().localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (delay <= 0)
        {
            speed = transform.parent.GetComponent<Rigidbody>().velocity.magnitude;
            AddMass(mass * speed * 0.00005f);
            AddSize(speed * 0.0001f);
        }
        else
        {
            delay = delay - 1;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        collidingObject = other.gameObject;

        if (collidingObject.layer == 8)
        {
            if (collidingObject.GetComponentInParent<Rigidbody>() != null)
            {
                Destroy(collidingObject.GetComponentInParent<NavMeshAgent>());
                Destroy(collidingObject.GetComponentInParent<Rigidbody>());
            } 
            else
            {
                Transform parent = collidingObject.transform.parent.parent.parent;
                RemoveCollidersRecursively(parent);
                parent.parent = transform.parent;
                AddMass(0.5f);
                AddSize(1f);
            }
        }
    }
    /*private void OnCollisionEnter(Collision collision)
    {
        collidingObject = collision.gameObject;
        Debug.Log("collision Detected with collider");

        if (collidingObject.GetComponentInParent<Rigidbody>() == null)
        {
            //Colliding with a snowman
            if (collidingObject.layer == 8)
            {
                Debug.Log("collision Detected");
                Transform parent = collidingObject.transform.parent.parent.parent;
                RemoveCollidersRecursively(parent);
                parent.parent = transform.parent;
                AddMass(0.5f);
                AddSize(1f);

            }
        }


    }*/

    private void RemoveCollidersRecursively(Transform parent)
    {
        var allColliders = parent.gameObject.GetComponentsInChildren<Collider>();

        foreach (var childCollider in allColliders) Destroy(childCollider);
    }

    //Increases the size of the snowball based on its original size
    private void AddSize(float addedSize)
    {
        transform.localScale = transform.localScale + size * addedSize;
    }

    //Adds mass to the snowball
    private void AddMass(float addedMass)
    {
        transform.parent.GetComponent<Rigidbody>().mass = transform.parent.GetComponent<Rigidbody>().mass + addedMass;
    }
}
