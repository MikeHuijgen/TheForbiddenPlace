using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSource : MonoBehaviour
{
    public void DealDamage(Health target, float damage)
    {
        target.TakeDamage(damage);
    }
}
