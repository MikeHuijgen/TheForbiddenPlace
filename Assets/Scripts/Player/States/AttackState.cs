using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class AttackState : MonoBehaviour, State
{
    [SerializeField] private List<AttackSO> combo;
    [SerializeField] private float secondsBetweenAttacks;
    [SerializeField] private int comboStepIndex;

    private StateMachine _stateMachine;
    private Animator _animator;
    private AnimatorOverrideController _animatorOverrideController;

    public UnityEvent startAttacking;
    public UnityEvent endAttacking;
    
    public bool hasSwappedAttack;
    public bool maySawp = true;
    public AttackSO AttackSo;
    
    private readonly string[] _attackStateNames = { "Attack1", "Attack2" };
    public int _attackStateId;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
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
        if (comboStepIndex >= combo.Count || !maySawp) return;
        maySawp = false;
        StopAllCoroutines();
        SwitchAttackAnimation();
        AttackSo = combo[comboStepIndex];
        comboStepIndex++;
        hasSwappedAttack = true;
        _animator.SetTrigger("Attack");
    }

    private void SwitchAttackAnimation()
    {
        var currentAttackStateName = _attackStateNames[_attackStateId];
        _animatorOverrideController[currentAttackStateName] = combo[comboStepIndex].attackAnimation;
        _attackStateId = _attackStateId == 1 ? 0 : 1;
    }

    public void StartedNewAttack()
    {
        hasSwappedAttack = false;
        maySawp = true;
    }

    public void AttackEnded()
    {
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
