using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : MonoBehaviour, State
{
    private StateMachine _stateMachine;
    private Animator _animator;

    public void Enter(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
        _animator = GetComponentInChildren<Animator>();
        _animator.SetBool("Idle", true);
    }

    public void Tick()
    {
        if (InputHandler.Instance.GetMovementValue() != Vector2.zero)
        {
            _stateMachine.SwitchState(playerState.Walk);
        }

        if (InputHandler.Instance.IsAttacking())
        {
            _stateMachine.SwitchState(playerState.Attack);
        }
    }

    public void Exit()
    {
        _animator.SetBool("Idle", false);
    }
}
