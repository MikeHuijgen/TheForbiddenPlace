using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSource : MonoBehaviour
{
    public float damage;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(damage);
    }
}
