using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class AttackState : MonoBehaviour, State
{
    [SerializeField] private List<AttackSO> combo;
    [SerializeField] private float secondsBetweenAttacks;

    private StateMachine _stateMachine;
    private Animator _animator;
    private Rigidbody _rigidbody;
    private float _lastAttackTime = 100;
    private float _lastComboEnd;
    private int _comboCounter;

    public UnityEvent startAttacking;
    public UnityEvent endAttacking;

    public bool maySwapAttack = true;


    public void Enter(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
        _animator = GetComponentInChildren<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _comboCounter = 0;
        _animator.ResetTrigger("DoneAttacking");
        Attack();
    }

    public void Tick()
    {
        _lastAttackTime += Time.deltaTime;
        if (InputHandler.Instance.IsAttacking())
        {
            Attack();
        }
    }

    public void Exit()
    {
        _rigidbody.velocity = Vector3.zero;
        _comboCounter = 0;
    }

    private void Attack()
    {
        if (_comboCounter > combo.Count || !maySwapAttack) return;
        CancelInvoke(nameof(EndCombo));
        maySwapAttack = false;
        startAttacking?.Invoke();
        if (_comboCounter >= combo.Count)
        {
            _comboCounter = 0;
        }
        _rigidbody.velocity = transform.forward * combo[_comboCounter].attackMoveForce;
        _animator.runtimeAnimatorController = combo[_comboCounter].animatorOverrideController;
        _animator.Play("Attack",0,0);
        _comboCounter++;
        _lastAttackTime = 0;
    }

    public void StartedNewAttack()
    {
        maySwapAttack = true;
    }

    public void ExitAttack()
    {
        Invoke(nameof(EndCombo), secondsBetweenAttacks + .2f);
    }

    private void EndCombo()
    {
        endAttacking?.Invoke();
        _animator.SetTrigger("DoneAttacking");
        if (InputHandler.Instance.GetMovementValue() != Vector2.zero)
        {
            _stateMachine.SwitchState(playerState.Walk);
        }
        else
        {
            _stateMachine.SwitchState(playerState.Idle);
        }
    }
}
