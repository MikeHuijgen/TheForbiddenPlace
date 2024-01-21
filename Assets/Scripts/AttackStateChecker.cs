using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackStateChecker : StateMachineBehaviour
{
    private AttackState _attackState;
    private bool _isFinished;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _isFinished = false;
        _attackState = animator.GetComponentInParent<AttackState>();
        _attackState.StartedNewAttack();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_isFinished || !(stateInfo.normalizedTime >= .9)) return;
        _isFinished = true;
        _attackState.EndAttack();
    }
}
