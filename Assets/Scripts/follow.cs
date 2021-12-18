using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class follow : MonoBehaviour
{
    Transform goal = default;
    NavMeshAgent agent;

    void Start()
    {
        goal = FindObjectOfType<AttackSpells>().GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        agent.SetDestination(goal.position);
    }
}
