using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestOverlap : MonoBehaviour
{
    public float range;
    public LayerMask enemyLayer;
    
    public void ActivateSphere()
    {
        var collider = Physics.OverlapSphere(transform.position, range, enemyLayer);
        foreach (var col in collider)
        {
            Debug.Log(col.name);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
