using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] float chaseRange = 5f;
    [SerializeField] float turnSpeed = 5f;

    NavMeshAgent navMeshAgent;
    float distanceToTarget = Mathf.Infinity;
    bool isProvoked = false;
    EnemyHealth health;
    Transform target;
    CapsuleCollider zombieCollider;

    public AudioSource groanAudio;
    public AudioSource bulletImpact;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        health = GetComponent<EnemyHealth>();
        target = FindObjectOfType<PlayerHealth>().transform;
        zombieCollider = GetComponent<CapsuleCollider>();

        AudioSource[] sounds = GetComponents<AudioSource>();
        bulletImpact = sounds[0];
        groanAudio = sounds[1];
    }

    // Update is called once per frame
    void Update()
    {
        if (health.IsDead())
        {
            groanAudio.Stop();
            enabled = false;
            navMeshAgent.enabled = false;
            zombieCollider.enabled = false;
        }

        distanceToTarget = Vector3.Distance(target.position, transform.position);

        if (isProvoked)
        {
            EngageTarget();
        }
        else if(distanceToTarget <= chaseRange)
        {
            isProvoked = true;
            groanAudio.Play();
        }
    }

    public void OnDamageTaken()
    {
        isProvoked = true;
    }


    // Is the enemy close enough to attack
    private void EngageTarget()
    {
        FaceTarget();
        if(distanceToTarget >= navMeshAgent.stoppingDistance)
        {
            ChaseTarget();
        }
        if(distanceToTarget <= navMeshAgent.stoppingDistance)
        {
            AttackTarget();
        }
    }

    // Enemy persues player
    private void ChaseTarget()
    {
        GetComponent<Animator>().SetBool("attack", false);
        GetComponent<Animator>().SetTrigger("move");
        navMeshAgent.SetDestination(target.position);
    }

    // Attack animation
    private void AttackTarget()
    {
        GetComponent<Animator>().SetBool("attack", true);
    }

    // Rotate the enemy to face the player when the player is inside the attack range
    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }


    // To visualize the enemy chase radius
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }

}
