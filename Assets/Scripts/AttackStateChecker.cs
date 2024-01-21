using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackStateChecker : StateMachineBehaviour
{
    private AttackState _attackState;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _attackState = animator.GetComponentInParent<AttackState>();
        _attackState.StartedNewAttack();
    }
}
