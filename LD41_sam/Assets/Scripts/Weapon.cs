using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

	public enum AttackMode { MELEE, RANGE }
    private AttackMode attackMode;

    public AttackMode Mode { get { return attackMode; } }

    public int attackDamage;
    public float reloadTime, attackRange;

}
