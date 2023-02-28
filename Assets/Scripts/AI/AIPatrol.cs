using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIPatrol : MonoBehaviour
{
    NavMeshAgent agent;
    protected Transform player;
    public LayerMask groundMask, playerMask;

    Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    protected bool alreadyAttacked;
    protected AIComponent ai;

    private void Awake()
    {
        if(!player)
            player = GameObject.Find("Character").transform;
        TryGetComponent(out agent);
        TryGetComponent(out ai);
    }

    // Update is called once per frame
    void Update()
    {
        ai.playerInSightRange = Physics.CheckSphere(transform.position, ai.sightRange, playerMask);
        ai.playerInAttackRange = Physics.CheckSphere(transform.position, ai.attackRange, playerMask);

        StateMachine();
    }
    protected void StateMachine()
    {
        if (!ai.playerInSightRange && !ai.playerInAttackRange)
            Patroling();
        if (ai.playerInSightRange && !ai.playerInAttackRange)
            ChasePlayer();
        if (ai.playerInSightRange && ai.playerInAttackRange)
            AttackPlayer();
    }

    protected void Patroling()
    {
        if (!walkPointSet)
            SearchWalkPoint();
        else
            agent.SetDestination(walkPoint);

        Vector3 distanceToWP = transform.position - walkPoint;

        // Ha llegado al punto
        if (distanceToWP.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        float randomZ = randomX;

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        // Comprobar que el nuevo waypoint no esté fuera del mapa
        if (Physics.Raycast(walkPoint, -transform.up, 2f, groundMask))
            walkPointSet = true;

    }

    void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    protected void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        if(!alreadyAttacked)
        {
            ai.Attack();

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), ai.timeBetweenAttacks);
        }
    }

    protected void ResetAttack()
    {
        alreadyAttacked = false;
    }
}
