using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitChecker : MonoBehaviour
{
    [SerializeField] private List<GameObject> hitColliders = new List<GameObject>();

    [SerializeField] private Collider weaponCollider;

    private void OnTriggerEnter(Collider other)
    {
        if (!hitColliders.Contains(other.gameObject))
        {
            hitColliders.Add(other.gameObject);
        }
    }
}
