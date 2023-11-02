using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private State _currentState;

    private playerState _playerState;
    
    private WalkState _walkState;
    private IdleState _idleState;

    private void Awake()
    {
        _walkState = GetComponent<WalkState>();
        _idleState = GetComponent<IdleState>();
    }

    private void Start()
    {
        _currentState = _idleState;
        _currentState.Enter(this);
    }

    private void Update()
    {
        _currentState.Tick();
    }

    private void FixedUpdate()
    {
        _currentState?.FixedTick();
    }

    public void SwitchState(playerState newState)
    {
        _currentState?.Exit();
        CheckNewState(newState);
        _currentState.Enter(this);
    }

    private void CheckNewState(playerState state)
    {
        switch (state)
        {
            case playerState.Idle:
                _currentState = _idleState;
                break;
            case playerState.Walk:
                _currentState = _walkState;
                break;
        }
    }
}
