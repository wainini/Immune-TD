using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using System;

public class Unit : MonoBehaviour
{
    public Action<int, int> OnHPChange;

    [SerializeField] protected UnitStats stats;

    protected int currentHP;
    public int CurrentHP { get { return currentHP; } protected set { currentHP = value; OnHPChange?.Invoke(currentHP, stats.MaxHP); } }


    private void Awake()
    {
        CurrentHP = stats.MaxHP;
    }

    public virtual void TakeDamage(int value)
    {
        CurrentHP = Mathf.Clamp(CurrentHP-value,0, stats.MaxHP);

        if(CurrentHP <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
