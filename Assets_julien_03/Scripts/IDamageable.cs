﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable {

    void TakeHit(int damage);
    void TakeDamage(int amount);
    void Die();
}
