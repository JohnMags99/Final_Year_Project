using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyAI : MonoBehaviour
{
    public LayerMask whatIsGround, whatIsPlayer;
    public float idleTime;
    public NavMeshAgent enemy;
    public Transform player;
    public GameObject projectile;

    //Enemy Patrolling
    public Vector3 walkPoint;
    private bool walkPointSet, isIdle;
    public float walkPointRange;

    //Attack
    public float attackTime;
    private bool alreadyAttacked;

    //State Change
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Update()
    {
        //Check for Ranges
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) EnemyPatrolling();
        if (playerInSightRange && !playerInAttackRange) Chase();
        if (playerInAttackRange) Attack();
    }

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        enemy = GetComponent<NavMeshAgent>();
    }

    private void EnemyPatrolling()
    {
        /*if (!walkPointSet)
            Invoke(nameof(SearchWalkPoint), idleTime);*/
        
        if (walkPointSet) 
            enemy.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        
        //Destination Reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void Chase()
    {
        enemy.SetDestination(player.position);
    }

    private void Attack()
    {
        //Don't move enemy
        enemy.SetDestination(transform.position);
        
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            GameObject bullet = Instantiate(projectile, transform.position, Quaternion.identity);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);
            Destroy(bullet,1f);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), attackTime);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
