using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PenguinNavigation : MonoBehaviour
{
    
    public Transform goal;
    Vector3 start;
    Vector3 fleeLocation;

    float distanceToGoal;

    int updatePath;

    int updateTime = 5;
    float movementSpeed = 1;

    bool running = false;
    bool turn = false;

    NavMeshAgent agent;
    Animation anim;


    void Start()
    {
        updatePath = 0;
        start = transform.position;
        distanceToGoal = 100;


        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animation>();
    }

    private void Update()
    {
        // Only update the path planning of the penguin every couple of frames to slow down the penguins movement
        agent = GetComponent<NavMeshAgent>();
        if (updatePath <= 0)
        {
            // For when the penguin is fleeing from the player snowball instead of normal path planning
            if (running)
            {
                anim.Play("run");
                agent.speed = movementSpeed*2f;
                agent.destination = fleeLocation;
            }
            else 
            {
                anim.Play("walk");
                if (turn)
                {
                    agent.speed = movementSpeed;
                    agent.destination = start;
                    updatePath = updateTime;

                    distanceToGoal = Mathf.Abs(transform.position.x - start.x) + Mathf.Abs(transform.position.z - start.z);
                }
                else
                {
                    agent.speed = movementSpeed;
                    agent.destination = goal.position;
                    updatePath = updateTime;

                    distanceToGoal = Mathf.Abs(transform.position.x - goal.position.x) + Mathf.Abs(transform.position.z - goal.position.z);
                }
                // Checks the distance to the goal and turns the penguin to walk back towards its start position once it gets close enough
                if (distanceToGoal < 10.0f)
                {
                    turn = !turn;
                }
            }
            
        }
        else
        {
            updatePath = updatePath - 1;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // Check within trigger radius for the player snowball or falling rock, then run away
        if (other.gameObject.layer == 10 || other.gameObject.layer == 11)
        {
            if (updatePath <= 0)
            {
                running = true;
                fleeLocation = transform.position - (other.gameObject.transform.position - transform.position);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // If snowball or rock is gone, resume normal path planning
        if (other.gameObject.layer == 10 || other.gameObject.layer == 11)
        {
            running = false;
        }
    }

}

