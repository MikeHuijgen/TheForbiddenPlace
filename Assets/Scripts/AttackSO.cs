using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Attacks/NormalAttack")]
public class AttackSO : ScriptableObject
{
    public AnimatorOverrideController animatorOverrideController;
    public float damage;
    public float attackMoveForce;
}
