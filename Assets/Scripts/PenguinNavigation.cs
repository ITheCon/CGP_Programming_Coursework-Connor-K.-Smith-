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
        agent = GetComponent<NavMeshAgent>();
        if (updatePath <= 0)
        {
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

                    distanceToGoal = Mathf.Abs(transform.position.x - start.x) + Mathf.Abs(transform.position.y - start.y);
                }
                else
                {
                    agent.speed = movementSpeed;
                    agent.destination = goal.position;
                    updatePath = updateTime;

                    distanceToGoal = Mathf.Abs(transform.position.x - goal.position.x) + Mathf.Abs(transform.position.y - goal.position.y);
                }
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
        if (other.gameObject.layer == 10)
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
        if (other.gameObject.layer == 10)
        {
            running = false;
        }
    }

}

