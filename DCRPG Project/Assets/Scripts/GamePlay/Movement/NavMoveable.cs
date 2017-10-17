using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NavMoveable : MonoBehaviour
{

    private NavMeshAgent p_Agent;

    private void Awake()
    {
        p_Agent = GetComponent<NavMeshAgent>();
        if (!p_Agent)
            Debug.LogError("NavMoveable: Awake: Failed to get NavMeshAgent on this object!");
    }

    virtual protected void SetDestination(Vector3 coords)
    {
        //This will have to calculate a path and move correctly, for now using built in command
        p_Agent.SetDestination(coords);
        
    }
}