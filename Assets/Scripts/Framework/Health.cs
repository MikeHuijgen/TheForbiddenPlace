using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class Health : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private bool isIndestructible;

    public UnityEvent OnTakeDamage = new UnityEvent();
    public void TakeDamage(float damage)
    {
        health -= damage;
        OnTakeDamage?.Invoke();
        if (health <= 0 && !isIndestructible)
        {
            Destroy(gameObject);
        }
    }

    public float GetCurrentHealth()
    {
        return health;
    }
}
