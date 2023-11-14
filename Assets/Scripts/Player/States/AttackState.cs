using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class AttackState : MonoBehaviour, State
{
    [SerializeField] private float attackMoveForce;
    [SerializeField] private GameObject hitPoint;

    private StateMachine _stateMachine;
    private Animator _animator;
    private Rigidbody _rigidbody;

    //Dit is de combo kijken of het nog gesplitst moet worden
    [SerializeField] private List<AttackSO> combo;
    
    private float _lastAttackTime = 100;
    private float _lastComboEnd;
    public int _comboCounter;
    private bool _canStartNewCombo = true;
    [SerializeField] private float secondsBetweenAttacks;
    private DamageSource _damageSource;

    public UnityEvent startAttacking;
    public UnityEvent endAttacking; 


    public void Enter(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
        _animator = GetComponentInChildren<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _comboCounter = 0;
        _damageSource = hitPoint.GetComponent<DamageSource>();
        Attack();
    }

    public void Tick()
    {
        _lastAttackTime += Time.deltaTime;
        if (InputHandler.Instance.IsAttacking())
        {
            Attack();
        }
        ExitAttack();
    }

    public void Exit()
    {
        _rigidbody.velocity = Vector3.zero;
        _comboCounter = 0;
    }

    private void Attack()
    {
        if (_comboCounter > combo.Count || !(_lastAttackTime >= secondsBetweenAttacks)) return;
        CancelInvoke(nameof(EndCombo));
        if (_comboCounter >= combo.Count)
        {
            _comboCounter = 0;
        }
        _rigidbody.velocity = transform.forward * attackMoveForce;
        _animator.runtimeAnimatorController = combo[_comboCounter].AnimatorOverrideController;
        _damageSource.damage = combo[_comboCounter].Damage;
        _animator.Play("Attack",0,0);
        startAttacking?.Invoke();
        _comboCounter++;
        _lastAttackTime = 0;
    }

    private void ExitAttack()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9 && _animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            Invoke(nameof(EndCombo), secondsBetweenAttacks + .2f);
        }
    }

    private void EndCombo()
    {
        endAttacking?.Invoke();
        _stateMachine.SwitchState(playerState.Idle);
    }
}
