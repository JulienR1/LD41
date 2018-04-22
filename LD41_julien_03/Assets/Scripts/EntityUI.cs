using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntityUI : MonoBehaviour {

    public HpBar hpBar;

    //----------------------------------------------------------------------------------------------------
    // Notify the user that a new weapon was selected
    //----------------------------------------------------------------------------------------------------
    public void SelectWeapon(Weapon weapon)
    {

    }
    
    //----------------------------------------------------------------------------------------------------
    // Notify the user that the entity attacks
    //----------------------------------------------------------------------------------------------------
    public void Attack()
    {

    }

    //----------------------------------------------------------------------------------------------------
    // Notify the user that the entity is taking damage
    //----------------------------------------------------------------------------------------------------
    public void TakeDamage(int amount, float healthPercent)
    {
        hpBar.SetVisibility(true);
        hpBar.UpdateValue(healthPercent);
    }

    //---------------------------------------------------------------------------------------------------
    // Notify the user that the entity died
    //---------------------------------------------------------------------------------------------------
    public void Die()
    {

    }
}
