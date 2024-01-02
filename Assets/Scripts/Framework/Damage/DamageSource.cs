using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSource : MonoBehaviour
{
    public float range = 2f;

    public void DealDamage(List<GameObject> targets, float damage)
    {
        //Debug.Log(damage);
        foreach (var target in targets)
        {
            if (target.TryGetComponent<Health>(out var health))
            {
                health.TakeDamage(damage);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
