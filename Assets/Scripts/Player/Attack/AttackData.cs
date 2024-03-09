using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Attacks/NormalAttack")]
public class AttackData : ScriptableObject
{
    public float damage;
    public float attackMoveForce;
    public float comboExitTime;
    public AnimationClip attackAnimation;
}
