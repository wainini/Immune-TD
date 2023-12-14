using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Immune Cell", menuName = "ImmuneCell")]
public class ImmuneCellData : ScriptableObject
{
    public int Cost;
    public float DeployCooldown;
}
