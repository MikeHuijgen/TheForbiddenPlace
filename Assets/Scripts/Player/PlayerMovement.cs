using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;
    
    private Rigidbody _rigidbody;

    private PlayerControls _playerControls;

    private Vector2 _movement;

    private void Awake()
    {
        _playerControls = new PlayerControls();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _playerControls.Movement.Enable();
    }


    private void Update()
    {
        _movement = _playerControls.Movement.Move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Move();
        RotatePlayer();
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
