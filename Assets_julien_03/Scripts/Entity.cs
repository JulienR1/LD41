using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour, IDamageable
{
    [Header("Default stats")]
    public int startingHealth = 100;
    public float startingMoveSpeed = 10;

    protected int currentHealth;
    protected float currentMoveSpeed;
    protected int currentAttackDamage;
    protected float currentReloadTime;
    protected float currentAttackRange;
    protected Weapon currentWeapon;
    protected float nextAttackTime;

    protected virtual void Start()
    {
        currentHealth = startingHealth;
        currentMoveSpeed = startingMoveSpeed;
        currentWeapon = null;
    }

    //---------------------------------------------------------------------------------------------------
    // Select a new weapoin and apply it's stats to the entity
    //---------------------------------------------------------------------------------------------------
    protected void SelectWeapon(Weapon weapon)
    {
        currentAttackDamage = weapon.attackDamage;
        currentReloadTime = weapon.reloadTime;
        currentAttackRange = weapon.attackRange;
    }

    //---------------------------------------------------------------------------------------------------
    // Attack another entity depending on the weapon and the reload time
    //---------------------------------------------------------------------------------------------------
    protected bool Attack(int damage, Entity enemy)
    {
        if (IsInRange(enemy.transform.position))
        {
            enemy.TakeDamage(damage);
            nextAttackTime = Time.time + currentReloadTime;
        }
        return false;
    }

    //----------------------------------------------------------------------------------------------------
    // Verify if the enemy is close enough depending on which weapon is selected
    //----------------------------------------------------------------------------------------------------
    private bool IsInRange(Vector3 enemyPos)
    {
        float dst = Vector3.Distance(transform.position, enemyPos);
        if (dst <= currentAttackRange)
        {
            // Needs to see the target to throw a projectile
            if (currentWeapon.Mode == Weapon.AttackMode.RANGE)
            {
                Ray ray = new Ray(transform.position, enemyPos - transform.position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, currentAttackRange))
                {
                    if (hit.transform.gameObject.layer != gameObject.layer)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        return false;
    }

    //---------------------------------------------------------------------------------------------------
    // Add effects or other stuff before hitting the entity
    //---------------------------------------------------------------------------------------------------
    public virtual void TakeHit(int damage)
    {
        // Effects here
        TakeDamage(damage);
    }

    //---------------------------------------------------------------------------------------------------
    // Actually affect the entity
    //---------------------------------------------------------------------------------------------------
    public virtual void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    //---------------------------------------------------------------------------------------------------
    // Remove the entity from the game
    //---------------------------------------------------------------------------------------------------
    public virtual void Die()
    {
        Destroy(gameObject);
    }
}