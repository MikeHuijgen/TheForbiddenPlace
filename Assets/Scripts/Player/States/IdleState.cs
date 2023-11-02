using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : MonoBehaviour, State
{
    private PlayerControls _playerControls;
    private StateMachine _stateMachine;

    public void Enter(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
        _playerControls = new PlayerControls();
        _playerControls.Movement.Enable();
    }

    public void Tick()
    {
        if (_playerControls.Movement.Move.triggered)
        {
            _stateMachine.SwitchState(playerState.Walk);
        }
    }
}
