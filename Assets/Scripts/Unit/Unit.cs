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
    protected Collider2D coll;
    protected int currentHP;
    public int CurrentHP { get { return currentHP; } protected set { currentHP = value; OnHPChange?.Invoke(currentHP, stats.MaxHP); } }

    protected SelectablePlot plot;

    protected virtual void Awake()
    {
        currentHP = stats.MaxHP;
        coll = GetComponent<Collider2D>();
    }

    public void EnableCollider()
    {
        if(coll != null)
            coll.enabled = true;
    }

    public void DisableCollider()
    {
        if (coll != null)
            coll.enabled = false;
    }

    public virtual void TakeDamage(int value)
    {
        CurrentHP = Mathf.Clamp(CurrentHP-value,0, stats.MaxHP);

        if(CurrentHP <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void SetPlot(SelectablePlot plot)
    {
        this.plot = plot;
    }
}
