using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : MonoBehaviour, State
{
    public bool isAttacking;
    private StateMachine _stateMachine;
    private PlayerControls _playerControls;
    private Animator _animator;

    public void Enter(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
        _playerControls = new PlayerControls();
        _playerControls.Movement.Disable();
        _animator = GetComponentInChildren<Animator>();
        _animator.SetTrigger("Idle");
    }

    public void Tick()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            
            //_stateMachine.SwitchState(playerState.Idle);
        }
    }

    public void Exit()
    {
        _animator.SetBool("Attack",false);
    }
}
