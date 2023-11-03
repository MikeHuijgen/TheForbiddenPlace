using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : MonoBehaviour, State
{
    [SerializeField] private float attackMoveForce;
    private StateMachine _stateMachine;
    private Animator _animator;

    public void Enter(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
        _animator = GetComponentInChildren<Animator>();
        _animator.SetTrigger("Attack");
    }

    public void Tick()
    {
        if (_animator.IsInTransition(0))
        {
            StartCoroutine(SwitchToIdleState());
        }
    }

    IEnumerator SwitchToIdleState()
    {
        yield return new WaitForSeconds(_animator.GetAnimatorTransitionInfo(0).duration);
        _stateMachine.SwitchState(playerState.Idle);
    }
}
