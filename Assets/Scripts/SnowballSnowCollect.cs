﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SnowballSnowCollect : MonoBehaviour
{
    float speed;
    float mass;
    float massToAdd = 0;
    float sizeToAdd = 0;
    Vector3 size;
    int delay = 120;
    GameObject collidingObject;
    // Start is called before the first frame update
    void Start()
    {
        mass = 1;
        size = new Vector3(1,1,1);
    }

    // Update is called once per frame
    void Update()
    {
        if (delay <= 0)
        {
            speed = transform.parent.GetComponent<Rigidbody>().velocity.magnitude;
            AddMass(mass * speed * 0.00015f);
            AddSize(speed * 0.0003f);

            if (massToAdd >= 0.025f)
            {
                AddMass(0.025f);
                massToAdd -= 0.025f;
            }
            if (sizeToAdd >= 0.05f)
            {
                AddSize(0.05f);
                sizeToAdd -= 0.05f;
            }
        }
        else
        {
            delay = delay - 1;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        collidingObject = other.gameObject;
        // Snowman Collision
        if (collidingObject.layer == 8)
        {
            if (GetComponentInParent<Rigidbody>().mass > 2)
            {
                if (collidingObject.GetComponentInParent<Rigidbody>() != null)
                {
                    Destroy(collidingObject.GetComponentInParent<NavMeshAgent>());
                    Destroy(collidingObject.GetComponentInParent<Rigidbody>());
                }
                else
                {
                    Transform parent = collidingObject.transform.parent.parent;
                    SetLayerOfChildren(parent.gameObject);
                    RemoveCollidersRecursively(parent);
                    parent.parent = transform.parent;
                    massToAdd += 0.5f;
                    sizeToAdd += 1f;
                }
            }
        }

        // Penguin Collision
        if (collidingObject.layer == 9)
        {
            if (GetComponentInParent<Rigidbody>().mass > 1.5)
            {
                if (collidingObject.GetComponentInParent<Rigidbody>() != null)
                {
                    //if (collidingObject.GetComponentInParent<NavMeshAgent>() != null)
                    Destroy(collidingObject.GetComponentInParent<NavMeshAgent>());
                    Destroy(collidingObject.GetComponentInParent<Rigidbody>());
                    Destroy(collidingObject.GetComponentInParent<Animation>());
                    Destroy(collidingObject.GetComponentInParent<PenguinNavigation>());

                }
                else
                {
                    Transform parent = collidingObject.transform.parent.parent;
                    SetLayerOfChildren(parent.gameObject);
                    RemoveCollidersRecursively(parent);
                    if (parent != null)
                        parent.parent = transform.parent;
                    massToAdd += 0.2f;
                    sizeToAdd += 0.4f;
                }
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
        if (parent != null)
        {
            var allColliders = parent.gameObject.GetComponentsInChildren<Collider>();

            foreach (var childCollider in allColliders) Destroy(childCollider);
        }
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

    public static void SetLayerOfChildren(GameObject go)
    {
        foreach (Transform trans in go.GetComponentsInChildren<Transform>(true))
        {
            trans.gameObject.layer = 10;
        }
    }
}
