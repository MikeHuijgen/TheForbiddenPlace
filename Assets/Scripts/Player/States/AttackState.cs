using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class AttackState : MonoBehaviour, State
{
    [SerializeField] private List<AttackData> combo;
    [SerializeField] private AttackData currentComboAttack;

    private int _comboStepIndex;
    private bool _maySwapAttack = true;
    private bool _isPreformingAttack;

    private StateMachine _stateMachine;
    private Animator _animator;
    private AnimatorOverrideController _animatorOverrideController;
    private Rigidbody _rigidbody;
    private HitChecker _hitChecker;

    public UnityEvent startAttacking;
    public UnityEvent endAttacking;

    public AttackData CurrentComboAttack => currentComboAttack;
    private bool _lastAttack => _comboStepIndex == combo.Count;


    private readonly string[] _attackStateNames = { "Attack1", "Attack2" };
    private int _attackStateId;

    private void Awake()
    {
        _stateMachine = GetComponent<StateMachine>();
        _animator = GetComponentInChildren<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _hitChecker = GetComponentInChildren<HitChecker>();
        _animatorOverrideController = new AnimatorOverrideController(_animator.runtimeAnimatorController);
        _animator.runtimeAnimatorController = _animatorOverrideController;
        _stateMachine.AddPlayerState(playerState.Attack, this);
    }


    public void Enter()
    {
        Attack();
    }

    public void Tick()
    {
        if (InputHandler.Instance.IsAttacking())
        {
            Attack();
        }

        if (InputHandler.Instance.IsDodging())
        {
            _stateMachine.SwitchState(playerState.Dodge);
        }
    }

    public void FixedTick()
    {
        if (!_isPreformingAttack) return;
        
        _hitChecker.CheckHits();
    }

    private void Attack()
    {
        if (_lastAttack || !_maySwapAttack) return;
        StopAllCoroutines();
        _maySwapAttack = false;
        SwitchAttackAnimation();
        _animator.SetTrigger("Attack");
    }

    private void SwitchAttackAnimation()
    {
        var currentAttackStateName = _attackStateNames[_attackStateId];
        _animatorOverrideController[currentAttackStateName] = combo[_comboStepIndex].attackAnimation;
        _attackStateId = _attackStateId == 1 ? 0 : 1;
    }

    private void AddAttackForce()
    {
        _rigidbody.velocity += transform.forward * combo[_comboStepIndex].attackMoveForce;
    }

    public void StartedNewAttack()
    {
        startAttacking?.Invoke();
        _isPreformingAttack = true;
        currentComboAttack = combo[_comboStepIndex];
        _maySwapAttack = true;
        AddAttackForce();
        StartCoroutine(EndCombo());
        _comboStepIndex++;
    }

    public void EndAttack()
    {
        endAttacking?.Invoke();
        _isPreformingAttack = false;
        _animator.SetTrigger("AttackEnded");
    }

    private IEnumerator EndCombo()
    {
        yield return new WaitForSeconds(currentComboAttack.comboExitTime);
        _animator.ResetTrigger("Attack");
        _animator.ResetTrigger("AttackEnded");
        _animator.SetTrigger("ComboEnded");
        _comboStepIndex = 0;
        _attackStateId = 0;
        _stateMachine.SwitchState(InputHandler.Instance.GetMovementDirectionValue() != Vector2.zero
            ? playerState.Idle
            : playerState.Walk);
    }
}
