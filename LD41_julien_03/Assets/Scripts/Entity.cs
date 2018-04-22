using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EntityUI))]
public class Entity : MonoBehaviour, IDamageable
{
    protected EntityUI entityUI;

    [Header("Default stats")]
    public int startingHealth = 100;
    public float startingMoveSpeed = 10;
    public bool invicible;

    protected int currentHealth;
    protected float currentMoveSpeed;

    protected int currentAttackDamage;
    protected float currentReloadTime;
    protected float currentAttackRange;
    protected Weapon currentWeapon;
    private float nextAttackTime;

    protected bool isAttacking;
    private bool isAlive;
    public bool IsAlive { get { return isAlive; } }

    protected virtual void Start()
    {
        currentHealth = startingHealth;
        currentMoveSpeed = startingMoveSpeed;
        currentWeapon = null;

        isAlive = true;
        isAttacking = false;

        entityUI = GetComponent<EntityUI>();
    }

    //---------------------------------------------------------------------------------------------------
    // Select a new weapon and apply it's stats to the entity
    //---------------------------------------------------------------------------------------------------
    protected void SelectWeapon(Weapon weapon)
    {
        currentWeapon = weapon;
        currentAttackDamage = weapon.attackDamage;
        currentReloadTime = weapon.reloadTime;
        currentAttackRange = weapon.attackRange;

        entityUI.SelectWeapon(currentWeapon);
    }

    //---------------------------------------------------------------------------------------------------
    // Attack another entity depending on the weapon and the reload time
    //---------------------------------------------------------------------------------------------------
    protected bool Attack(Entity enemy)
    {
        if (IsAlive && enemy.IsAlive && !isAttacking)
        {
            if (Time.time >= nextAttackTime)
            {
                if (IsInRange(enemy.transform.position))
                {
                    enemy.TakeHit(currentAttackDamage);
                    nextAttackTime = Time.time + currentReloadTime;

                    entityUI.Attack();
                    return true;
                }
            }
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
        if (!invicible)
            TakeDamage(damage);
    }

    //---------------------------------------------------------------------------------------------------
    // Actually affect the entity
    //---------------------------------------------------------------------------------------------------
    public virtual void TakeDamage(int amount)
    {
        currentHealth -= amount;
        entityUI.TakeDamage(amount, GetHpPercent());
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
        entityUI.Die();
        isAlive = false;
        GameObject.Destroy(gameObject);
    }

    //----------------------------------------------------------------------------------------------------
    // Gets the progress of the hp
    //----------------------------------------------------------------------------------------------------
    public float GetHpPercent()
    {
        return (float)currentHealth / (float)startingHealth;
    }
}