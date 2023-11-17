using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Attacks/NormalAttack")]
public class AttackSO : ScriptableObject
{
    public AnimatorOverrideController AnimatorOverrideController;
    public float Damage;
    public Vector3 HitPointLocation;
}
