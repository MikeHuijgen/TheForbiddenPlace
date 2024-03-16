using System.Collections.Generic;
using UnityEngine;

public class HitChecker : MonoBehaviour
{
    [SerializeField] private List<Collider> hitColliders;
    [SerializeField] private int layerToIgnore;
    private BoxCollider _hitBoxCollider;
    private DamageSource _damageSource;
    private AttackState _attackState;

    private void Awake()
    {
        _damageSource = GetComponent<DamageSource>();
        _attackState = GetComponentInParent<AttackState>();
        _hitBoxCollider = GetComponent<BoxCollider>();
    }

    public void CheckHits()
    {
        var hitBounds = _hitBoxCollider.bounds;

        var hitObjects = Physics.OverlapBox(hitBounds.center, hitBounds.size);

        foreach (var hit in hitObjects)
        {
            if (hitColliders.Contains(hit) || !hit.TryGetComponent<Health>(out var health) || hit.gameObject.layer == layerToIgnore) continue;
            
            hitColliders.Add(hit);
            _damageSource.DealDamage(health, _attackState.CurrentComboAttack.damage);
        }
    }

    public void ResetHitColliders()
    {
        hitColliders.Clear();
    }
}
