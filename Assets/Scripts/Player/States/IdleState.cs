using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : MonoBehaviour, State
{
    private PlayerControls _playerControls;
    private StateMachine _stateMachine;

    private Rigidbody _rigidbody;
    
    public void Enter(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
        _playerControls = new PlayerControls();
        _playerControls.Movement.Enable();
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.velocity = Vector3.zero;
    }

    public void Tick()
    {
        if (_playerControls.Movement.Move.triggered)
        {
            _stateMachine.SwitchState(playerState.Walk);
        }
        
        Debug.Log("Idle tick");
    }
}
