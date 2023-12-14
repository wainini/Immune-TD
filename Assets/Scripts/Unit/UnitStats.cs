using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New UnitStats", menuName = "UnitStats")]
public class UnitStats : ScriptableObject
{
    public int MaxHP;
    public int Damage;
    public float AttackCooldown;
    public float MoveSpeed;
}
