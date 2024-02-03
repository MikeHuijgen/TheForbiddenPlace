using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : MonoBehaviour, State
{
    private StateMachine _stateMachine;

    private void Awake()
    {
        _stateMachine = GetComponent<StateMachine>();
        _stateMachine.AddPlayerState(playerState.Idle, this);
    }

    public void Enter()
    {

    }

    public void Tick()
    {
        if (InputHandler.Instance.GetMovementDirectionValue() != Vector2.zero)
        {
            _stateMachine.SwitchState(playerState.Walk);
        }

        if (InputHandler.Instance.IsAttacking())
        {
            _stateMachine.SwitchState(playerState.Attack);
        }

        if (InputHandler.Instance.IsDodging())
        {
            _stateMachine.SwitchState(playerState.Dodge);
        }
    }
}
