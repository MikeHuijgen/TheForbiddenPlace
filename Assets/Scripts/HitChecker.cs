using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class HitChecker : MonoBehaviour
{
    [SerializeField] private List<Collider> hitColliders;
    [SerializeField] private List<LayerMask> layersToIgnore;
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
            if (!hitColliders.Contains(hit) && hit.GetComponent<Health>() && hit.gameObject.layer != LayerMask.NameToLayer("Player"))
            {
                hitColliders.Add(hit);
                print("k");
            }
            
            
            /*if (!IsValidTarget(hit) || !hit.TryGetComponent<Health>(out var health)) continue;
            hitColliders.Add(hit);
            _damageSource.DealDamage(health, _attackState.CurrentComboAttack.damage);*/
        }
    }

    private bool IsValidTarget(Collider target)
    {
        return hitColliders.Contains(target) || !TargetHasNoIgnoredLayer(target);
    }

    private bool TargetHasNoIgnoredLayer(Collider target)
    {
        foreach (var layer in layersToIgnore)
        {
            if (target.gameObject.layer == layer)
            {
                return true;
            }
        }

        return false;
    }

    public void ResetHitColliders()
    {
        hitColliders.Clear();
    }
}
