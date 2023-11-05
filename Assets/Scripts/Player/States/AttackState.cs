using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : MonoBehaviour, State
{
    [SerializeField] private float attackMoveForce;
    [SerializeField] private GameObject weapon;

    private StateMachine _stateMachine;
    private Animator _animator;
    private Rigidbody _rigidbody;

    private BoxCollider _weaponCollider;
    
    //Dit is de combo kijken of het nog gesplitst moet worden
    [SerializeField] private List<AttackSO> combo;
    private float _lastClickedTime;
    private float _lastComboEnd;
    public int _comboCounter;


    public void Enter(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
        _animator = GetComponentInChildren<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _weaponCollider = weapon.GetComponent<BoxCollider>();
        _rigidbody.velocity = transform.forward * attackMoveForce;
        _comboCounter = 0;
        Attack();
    }

    public void Tick()
    {
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
        _lastComboEnd = 1;
    }

    void Attack()
    {
        if (Time.time - _lastComboEnd > 0.5f && _comboCounter <= combo.Count)
        {
            CancelInvoke(nameof(EndCombo));

            if (Time.time - _lastClickedTime >= 0.2f)
            {
                _weaponCollider.enabled = true;
                _animator.runtimeAnimatorController = combo[_comboCounter].AnimatorOverrideController;
                _animator.Play("Attack",0,0);
                weapon.GetComponent<DamageSource>().damage = combo[_comboCounter].Damage;
                _comboCounter++;
                _lastClickedTime = Time.time;
                
                if (_comboCounter >= combo.Count)
                {
                    _comboCounter = 0;
                }
            }
        }
    }

    void ExitAttack()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9 && _animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            _weaponCollider.enabled = false;
            if (InputHandler.Instance.GetMovementValue() != Vector2.zero)
            {
                Invoke(nameof(EndCombo), 0.1f);
            }
            Invoke(nameof(EndCombo), .5f);
        }
    }

    void EndCombo()
    {
        _stateMachine.SwitchState(playerState.Idle);
    }
}
