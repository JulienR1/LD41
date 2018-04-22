using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : Entity {

    private NavMeshAgent agent;
    private Player player;

    [Header("Enemy settings")]
    public Weapon startWeapon;
    public float refreshRate = 0.2f;
    private float nextRefreshTime;
    public float maxDistanceToTarget = 20f;

    public Loot loot;

    protected override void Start()
    {
        base.Start();
        player = FindObjectOfType<Player>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = currentMoveSpeed;
        agent.stoppingDistance = currentAttackRange;
        SelectWeapon(startWeapon);
    }

    //---------------------------------------------------------------------------------------------------
    // Attack if possible the player otherwise move
    //---------------------------------------------------------------------------------------------------
    private void Update()
    {
        if (IsAlive && player.IsAlive)
        {
            isAttacking = Attack(player);
            float dst = Vector3.Distance(transform.position, player.transform.position);
            if (dst <= maxDistanceToTarget)
            {
                agent.isStopped = false;
                if (Time.time >= nextRefreshTime && !isAttacking)
                {
                    SetTarget(player);
                }
                return;
            }            
        }
        agent.isStopped = true;
    }

    //---------------------------------------------------------------------------------------------------
    // Lock in to the target
    //---------------------------------------------------------------------------------------------------
    private void SetTarget(Entity target)
    {
        if (IsAlive && target.IsAlive)
        {
            agent.SetDestination(player.transform.position);
            nextRefreshTime = Time.time + refreshRate;
        }
        else
        {
            agent.isStopped = true;
        }
    }

    public override void Die()
    {
        Inventory.AddLoot(loot, false);
        base.Die();
    }
}
