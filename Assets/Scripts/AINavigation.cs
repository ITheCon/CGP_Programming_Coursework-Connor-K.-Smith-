using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AINavigation : MonoBehaviour
{

    public Transform goal;
    int updatePath;
    public int updateTime = 10;

    void Start()
    {
        updatePath = 0;
    }

    private void Update()
    {
        if (updatePath <= 0)
        {
            UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
            agent.destination = goal.position;
            updatePath = updateTime;
        }
        else
        {
            updatePath = updatePath - 1;
        }
    }
        
}

