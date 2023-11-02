using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : MonoBehaviour, State
{
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;
    
    private Rigidbody _rigidbody;

    private PlayerControls _playerControls;

    private Vector2 _movement;
    
    private StateMachine _stateMachine;

    public void Enter(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
        _playerControls = new PlayerControls();
        _rigidbody = GetComponent<Rigidbody>();
        _playerControls.Movement.Enable();
    }

    public void Tick()
    {
        _movement = _playerControls.Movement.Move.ReadValue<Vector2>();

        if (_movement == Vector2.zero)
        {
            _stateMachine.SwitchState(playerState.Idle);
        }
    }

    public void FixedTick()
    {
        Move();
        RotatePlayer();
    }

    public void Exit()
    {
        _rigidbody.velocity = Vector3.zero;
    }

    private void Move()
    {
        _rigidbody.velocity = new Vector3(_movement.x * speed, 0, _movement.y * speed);
    }

    private void RotatePlayer()
    {
        if (_movement != Vector2.zero)
        {
            var targetRotation = Quaternion.LookRotation(new Vector3(_movement.x, 0, _movement.y));
            targetRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed);
            _rigidbody.MoveRotation(targetRotation);
        }
    }
}
