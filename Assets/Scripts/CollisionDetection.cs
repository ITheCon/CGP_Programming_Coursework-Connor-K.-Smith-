using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CollisionDetection : MonoBehaviour
{
    GameObject collidingObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        collidingObject = collision.gameObject;

        if (collidingObject.GetComponentInParent<Rigidbody>() == null)
        {
            //Colliding with a snowman
            if(collidingObject.layer == 8)
            {
                Debug.Log("collision Detected");
                Transform parent = collidingObject.transform.parent.parent.parent.parent;
                RemoveCollidersRecursively(parent);
                parent = gameObject.transform;
                transform.localScale = transform.localScale;

            }
        }

        
    }

    private void RemoveCollidersRecursively(Transform parent)
    {
        var allColliders = parent.GetComponentsInChildren<Collider>();

        foreach (var childCollider in allColliders) Destroy(childCollider);
    }
}
