using System.Collections;
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
    bool touchingGround = false;

    public float growWithSpeedMagnitude = 10;

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
        // Slowly add mass and size to the player snowball as they are moving on the ground
        if (touchingGround)
        {
            speed = transform.parent.GetComponent<Rigidbody>().velocity.magnitude;
            AddMass(mass * speed * 0.00001f * growWithSpeedMagnitude);
            AddSize(speed * 0.00002f * growWithSpeedMagnitude);
        }
        // A pool variable to add mass or size if there is a remaining amount left. Other objects touched either increase or decreases this pool. 
        if (massToAdd >= 0.025f)
        {
            AddMass(0.025f);
            massToAdd -= 0.025f;
        }
        else if (massToAdd <= -0.025f)
        {
            AddMass(-0.025f);
            massToAdd += 0.025f;
        }

        if (sizeToAdd >= 0.05f)
        {
            AddSize(0.05f);
            sizeToAdd -= 0.05f;
        }
        else if (sizeToAdd <= -0.05f)
        {
            AddSize(-0.05f);
            sizeToAdd += 0.05f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        collidingObject = other.gameObject;
        // Snowman Collision
        if (collidingObject.layer == 8)
        {
            if (GetComponentInParent<Rigidbody>().mass >= 3.5f)
            {
                if (collidingObject.GetComponentInParent<Rigidbody>() != null)
                {
                    Destroy(collidingObject.GetComponentInParent<NavMeshAgent>());
                    Destroy(collidingObject.GetComponentInParent<Rigidbody>());
                }
                else
                {
                    // set the parent of the object to the snowball instead and make it apart of the player
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
            if (GetComponentInParent<Rigidbody>().mass > 2.5f)
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
                    // set the parent of the object to the snowball instead and make it apart of the player
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

        // Rock Collision
        if (collidingObject.layer == 11)
        {
            massToAdd -= 0.1f;
            sizeToAdd -= 0.2f;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        collidingObject = other.gameObject;
        // Check if touching ground
        if (collidingObject.layer == 0)
            touchingGround = true;
    }
    private void OnTriggerExit(Collider other)
    {
        collidingObject = other.gameObject;
        // Check if touching ground
        if (collidingObject.layer == 0)
            touchingGround = false;
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

    // Removes all colliders of an object
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


    // Chnage the layer of all children to the player's layer
    public static void SetLayerOfChildren(GameObject go)
    {
        foreach (Transform trans in go.GetComponentsInChildren<Transform>(true))
        {
            trans.gameObject.layer = 10;
        }
    }
}
