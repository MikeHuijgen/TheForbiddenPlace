using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitChecker : MonoBehaviour
{
    [SerializeField] private List<GameObject> hitColliders = new List<GameObject>();
    [SerializeField] private Collider weaponCollider;

    public List<GameObject> HitColliders => hitColliders;

    private void OnTriggerEnter(Collider other)
    {
        if (!hitColliders.Contains(other.gameObject))
        {
            hitColliders.Add(other.gameObject);
        }
    }

    public void ResetHitColliders()
    {
        hitColliders.Clear();
    }
}
