using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName ="Weapon")]
public class Weapon : ScriptableObject {

	public enum AttackMode { MELEE, RANGE }
    private AttackMode attackMode;

    public AttackMode Mode { get { return attackMode; } }

    new public string name;
    public int attackDamage;
    public float reloadTime, attackRange;

}
