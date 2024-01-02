using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackStateChecker : StateMachineBehaviour
{
    private bool _attackFinished;
    private HitChecker _hitChecker;
    private AttackState _attackState;
    private DamageSource _damageSource;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _attackFinished = false;
        _hitChecker = animator.GetComponentInChildren<HitChecker>();
        _damageSource = animator.GetComponentInChildren<DamageSource>();
        _attackState = animator.GetComponentInParent<AttackState>();
        _attackState.StartedNewAttack();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.normalizedTime >= 1 && !_attackFinished)
        {
            _attackFinished = true;
            var targets = _hitChecker.HitColliders;
            _damageSource.DealDamage(targets, _attackState.CurrentComboAttack.damage);
            _attackState.AttackEnded();
        }
    }
}
