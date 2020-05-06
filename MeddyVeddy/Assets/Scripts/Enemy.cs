using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : LivingEntity
{
    public enum State { Idle, Chasing, Attacking };
    public State currentState;

    public bool isBoss = false;

    RangedWeaponController rwController;

    NavMeshAgent pathFinder;
    Transform target;
    GameObject player;
    LivingEntity targetEntity;
    Material skinMaterial;

    Color originalColor;

    public int roomID;

    //float detectionDistanceThreshold = 5f;
    float attackDistanceThreshold = 0.5f;
    float timeBetweenAttacks = 1;
    float damage = 1;

    float nextAttackTime;
    float myCollisionRadius;
    float targetCollisionRadius;

    bool hasTarget;
    public bool inSameRoom;

    protected override void Start()
    {
        base.Start();
        rwController = GetComponent<RangedWeaponController>();

        if (isBoss)
        {
            UpgradeToBoss();
        }

        pathFinder = GetComponent<NavMeshAgent>();
        skinMaterial = GetComponent<Renderer>().material;
        originalColor = skinMaterial.color;

        player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            currentState = State.Chasing;
            hasTarget = true;
            target = player.transform;
            targetEntity = target.GetComponent<LivingEntity>();
            targetEntity.OnDeath += OnTargetDeath;

            myCollisionRadius = GetComponent<CapsuleCollider>().radius;
            targetCollisionRadius = target.GetComponent<CapsuleCollider>().radius;

            StartCoroutine(UpdatePath());
        }
    }

    void UpgradeToBoss()
    {
        transform.localScale = transform.localScale * 3;
        startingHealth = 10;
        health = startingHealth;
        attackDistanceThreshold = 1.5f;
        GetComponent<NavMeshAgent>().speed = GetComponent<NavMeshAgent>().speed / 3;
    }

    void OnTargetDeath()
    {
        hasTarget = false;
        currentState = State.Idle;
    }

    void Update()
    {
        /// Updates the pathfinder only when the player is in the same room
        if (inSameRoom)
        {
            CheckRange();

            if (hasTarget)
            {
                if (Time.time > nextAttackTime)
                {
                    float sqrDstToTarget = (target.position - transform.position).sqrMagnitude; /* Distance in squared form */
                    if (sqrDstToTarget < Mathf.Pow(attackDistanceThreshold + myCollisionRadius + targetCollisionRadius, 2))
                    {
                        nextAttackTime = Time.time + timeBetweenAttacks;
                        StartCoroutine(Attack());
                    }
                    else
                    {
                        if (isBoss)
                        {
                            nextAttackTime = Time.time + timeBetweenAttacks;
                            rwController.BurstFire();
                        }
                    }
                }
            }
        }
        else
        {
            currentState = State.Idle;
            pathFinder.enabled = false;
        }
    }

    void CheckRange()
    {
        Debug.Log(Vector3.Distance(transform.position, target.position));
        if (Vector3.Distance(transform.position, target.position) <= 1)
        {
            currentState = State.Chasing;
            pathFinder.enabled = true;
        }
    }

    IEnumerator Attack()
    {
        currentState = State.Attacking;
        pathFinder.enabled = false; /* Stops the pathfinder while attacking */

        Vector3 originalPosition = transform.position;
        Vector3 dirToTarget = (target.position - transform.position).normalized;
        Vector3 attackPosition = target.position - dirToTarget * myCollisionRadius;

        float attackSpeed = 3;
        float percent = 0;

        skinMaterial.color = Color.red;
        bool hasAppliedDamage = false;

        while (percent <= 1)
        {
            if (percent >= 0.5f && !hasAppliedDamage)
            {
                hasAppliedDamage = true;
                targetEntity.TakeDamage(damage);
            }

            percent += Time.deltaTime * attackSpeed;
            float interpolation = (-Mathf.Pow(percent, 2) + percent) * 4;
            transform.position = Vector3.Lerp(originalPosition, attackPosition, interpolation);
            /* When interpolation is 0, we get the originalPosition, when interpolation is 1, we get the attackPosition */

            yield return null;
        }

        skinMaterial.color = originalColor;
        currentState = State.Chasing;
        pathFinder.enabled = true; /* Resumes the pathfinding after attacking */
    }

    /// Once called, the loop will go through every refreshRate
    /// To avoid recalculating the path every frame, which can be quite expensive
    IEnumerator UpdatePath()
    {
        float refreshRate = 0.05f; /* How often in seconds the agent will update its path */

        while (hasTarget)
        {
            if (currentState == State.Chasing)
            {
                Vector3 dirToTarget = (target.position - transform.position).normalized;
                Vector3 targetPosition = target.position - dirToTarget * (myCollisionRadius + targetCollisionRadius + attackDistanceThreshold / 2);
                if (!dead)
                {
                    pathFinder.SetDestination(targetPosition);
                }
                //else
                //{
                //    pathFinder.ResetPath();
                //}
            }
            yield return new WaitForSeconds(refreshRate);
        }
    }
}
