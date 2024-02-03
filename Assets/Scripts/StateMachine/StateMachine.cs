using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    [SerializeField] private playerState playerState;
    
    private State _currentState;
    private readonly Dictionary<playerState, State> _playerStates = new Dictionary<playerState, State>();

    private void Start()
    {
        _currentState = _playerStates[playerState];
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
        _currentState = _playerStates[newState];
        _currentState.Enter();
    }

    public void AddPlayerState(playerState playerState, State state)
    {
        if (_playerStates.ContainsKey(playerState)) return;

        _playerStates.Add(playerState, state);
    }
}
