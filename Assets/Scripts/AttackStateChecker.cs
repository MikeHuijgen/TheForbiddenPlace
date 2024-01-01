using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackStateChecker : StateMachineBehaviour
{
    private bool _attackFinished;
    private HitChecker _hitChecker;
    private AttackState _attackState;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _attackFinished = false;
        _hitChecker = animator.GetComponentInChildren<HitChecker>();
        _attackState = animator.GetComponentInParent<AttackState>();
        _attackState.StartedNewAttack();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.normalizedTime >= 1 && !_attackFinished)
        {
            _attackFinished = true;
            //_attackState.ExitAttack();
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _hitChecker.ResetHitColliders();
    }
}
