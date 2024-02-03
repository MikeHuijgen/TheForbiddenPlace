using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeState : MonoBehaviour, State
{
    [SerializeField] private float dodgeForce;
    private StateMachine _stateMachine;
    private Rigidbody _rigidbody;

    private Vector2 _movementDirection;
    private float timer;

    private void Awake()
    {
        _stateMachine = GetComponent<StateMachine>();
        _rigidbody = GetComponent<Rigidbody>();
        _stateMachine.AddPlayerState(playerState.Dodge, this);
    }

    public void Enter()
    {
        timer = 0;
        _movementDirection = InputHandler.Instance.GetMovementDirectionValue();
        RotatePlayer();
        AddForce();
    }

    public void Tick()
    {
        timer += Time.deltaTime;
        if (timer > 1)
        {
            _stateMachine.SwitchState(playerState.Idle);
        }
    }

    private void RotatePlayer()
    {
        if (_movementDirection == Vector2.zero) return;
        var targetRotation = Quaternion.LookRotation(new Vector3(_movementDirection.x, 0, _movementDirection.y));
        transform.rotation = targetRotation;
    }

    private void AddForce()
    {
        _rigidbody.velocity += transform.forward * dodgeForce;
    }
}
