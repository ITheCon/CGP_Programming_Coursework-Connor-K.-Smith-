using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinNavigation : MonoBehaviour
{

    public Transform goal;
    int updatePath;
    public int updateTime = 5;
    public float movementSpeed;

    void Start()
    {
        updatePath = 0;
    }

    private void Update()
    {
        if (updatePath <= 0)
        {
            UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
            agent.speed = 2;
            agent.destination = goal.position;
            updatePath = updateTime;
        }
        else
        {
            updatePath = updatePath - 1;
        }
    }

}

