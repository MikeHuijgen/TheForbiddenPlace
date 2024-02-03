using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : MonoBehaviour, State
{
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;
    
    private Rigidbody _rigidbody;
    private Vector2 _movement;
    private StateMachine _stateMachine;
    private Animator _animator;

    private float _lastMovementInput;
    private const float WaitTillIdle = .2f;

    private void Awake()
    {
        _stateMachine = GetComponent<StateMachine>();
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<Animator>();
        _stateMachine.AddPlayerState(playerState.Walk, this);
    }

    public void Enter()
    {
        _animator.SetBool("Walking", true);
    }

    public void Tick()
    {
        _movement = InputHandler.Instance.GetMovementDirectionValue();
        
        if (_movement == Vector2.zero)
        {
            _animator.SetBool("Walking", false);
            _lastMovementInput += Time.deltaTime;
            if (_lastMovementInput > WaitTillIdle)
            {
                _lastMovementInput = 0;
                _stateMachine.SwitchState(playerState.Idle);
            }
        }
        else
        {
            _lastMovementInput = 0;
            _animator.SetBool("Walking", true);
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

    public void FixedTick()
    {
        Move();
        RotatePlayer();
    }

    public void Exit()
    {
        _animator.SetBool("Walking", false);
        _rigidbody.velocity = Vector3.zero;
    }

    private void Move()
    {
        _rigidbody.velocity = new Vector3(_movement.x * speed, 0, _movement.y * speed);
    }

    private void RotatePlayer()
    {
        if (_movement == Vector2.zero) return;
        var targetRotation = Quaternion.LookRotation(new Vector3(_movement.x, 0, _movement.y));
        targetRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed);
        _rigidbody.MoveRotation(targetRotation);
    }
}
