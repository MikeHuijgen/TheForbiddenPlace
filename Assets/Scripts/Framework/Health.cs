using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Health : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private bool isIndestructible;
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0 && !isIndestructible)
        {
            Destroy(gameObject);
        }
    }
}
