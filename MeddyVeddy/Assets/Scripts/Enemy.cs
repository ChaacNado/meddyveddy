using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : LivingEntity
{
    public enum State { Idle, Chasing, Attacking};
    State currentState;

    NavMeshAgent pathFinder;
    Transform target;

    float attackDistanceThreshold = 1.5f;
    float timeBetweenAttacks = 1;

    float nextAttackTime;

    protected override void Start()
    {
        base.Start();
        pathFinder = GetComponent<NavMeshAgent>();

        currentState = State.Chasing;
        target = GameObject.FindGameObjectWithTag("Player").transform;

        StartCoroutine(UpdatePath());
    }

    void Update()
    {
        if (Time.time > nextAttackTime)
        {
            float sqrDstToTarget = (target.position - transform.position).sqrMagnitude; /* Distance in squared form */
            if (sqrDstToTarget < Mathf.Pow(attackDistanceThreshold, 2))
            {
                nextAttackTime = Time.time + timeBetweenAttacks;
                StartCoroutine(Attack());
            }
        }
    }

    IEnumerator Attack()
    {
        currentState = State.Attacking;
        pathFinder.enabled = false; /* Stops the pathfinder while attacking */

        Vector3 originalPosition = transform.position;
        Vector3 attackPosition = target.position;

        float attackSpeed = 3;
        float percent = 0;

        while (percent <= 1)
        {
            percent += Time.deltaTime * attackSpeed;
            float interpolation = (-Mathf.Pow(percent, 2) + percent) * 4;
            transform.position = Vector3.Lerp(originalPosition, attackPosition, interpolation);
            /* When interpolation is 0, we get the originalPosition, when interpolation is 1, we get the attackPosition */

            yield return null;
        }

        currentState = State.Chasing;
        pathFinder.enabled = true; /* Resumes the pathfinding after attacking */
    }

    /// Once called, the loop will go through every refreshRate
    /// To avoid recalculating the path every frame, which can be quite expensive
    IEnumerator UpdatePath()
    {
        float refreshRate = 0.25f; /* How often in seconds the agent will update its path */

        while (target != null)
        {
            if(currentState == State.Chasing)
            {
                Vector3 targetPosition = new Vector3(target.position.x, 0, target.position.z);
                if (!dead)
                {
                    pathFinder.SetDestination(targetPosition);
                }
                
            }
            yield return new WaitForSeconds(refreshRate);
        }
    }
}
