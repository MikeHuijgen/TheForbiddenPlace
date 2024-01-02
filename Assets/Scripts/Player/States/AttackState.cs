using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class AttackState : MonoBehaviour, State
{
    [SerializeField] private List<AttackSO> combo;
    private AttackSO currentComboAttack;
    
    private int comboStepIndex;
    private bool hasSwappedAttack;
    private bool maySwapAttack = true;

    private StateMachine _stateMachine;
    private Animator _animator;
    private AnimatorOverrideController _animatorOverrideController;
    private Rigidbody _rigidbody;
    private HitChecker _hitChecker;
    private DamageSource _damageSource;

    public UnityEvent startAttacking;
    public UnityEvent endAttacking;

    public AttackSO CurrentComboAttack => currentComboAttack;
    

    private readonly string[] _attackStateNames = { "Attack1", "Attack2" };
    public int _attackStateId;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _hitChecker = GetComponentInChildren<HitChecker>();
        _damageSource = GetComponentInChildren<DamageSource>();
        _animatorOverrideController = new AnimatorOverrideController(_animator.runtimeAnimatorController);
        _animator.runtimeAnimatorController = _animatorOverrideController;
    }


    public void Enter(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
        _animator.ResetTrigger("DoneAttacking");
        Attack();
    }

    public void Tick()
    {
        if (InputHandler.Instance.IsAttacking())
        {
            Attack();
        }
    }

    private void Attack()
    {
        if (comboStepIndex >= combo.Count || !maySwapAttack) return;
        maySwapAttack = false;
        StopAllCoroutines();
        currentComboAttack = combo[comboStepIndex];
        SwitchAttackAnimation();
        AddAttackForce();
        hasSwappedAttack = true;
        _animator.SetTrigger("Attack");
    }

    private void SwitchAttackAnimation()
    {
        var currentAttackStateName = _attackStateNames[_attackStateId];
        _animatorOverrideController[currentAttackStateName] = combo[comboStepIndex].attackAnimation;
        _attackStateId = _attackStateId == 1 ? 0 : 1;
    }

    private void AddAttackForce()
    {
        _rigidbody.velocity += transform.forward * combo[comboStepIndex].attackMoveForce;
    }

    public void StartedNewAttack()
    {
        startAttacking?.Invoke();
        comboStepIndex++;
        hasSwappedAttack = false;
        maySwapAttack = true;
    }

    public void AttackEnded()
    {
        endAttacking?.Invoke();

        if(hasSwappedAttack) return;
        _animator.SetTrigger("ComboEnded");
        comboStepIndex = 0;
        _attackStateId = 0;
        if (InputHandler.Instance.GetMovementValue() != Vector2.zero)
        {
            _stateMachine.SwitchState(playerState.Idle);
        }
        else
        {
            _stateMachine.SwitchState(playerState.Walk);
        }
    }
}
