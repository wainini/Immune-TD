using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neutrophil : ImmuneCell
{
    [SerializeField] private UnitDetection unitDetection;

    private Unit currentTarget;
    private float currentAttackCooldown = 0;

    private void Update()
    {
        SearchUnitToAttack();
    }
    private void FixedUpdate()
    {
        currentAttackCooldown -= Time.deltaTime;

        if(currentTarget != null)
        {
            AttackCurrentTarget();
        }
    }

    private void AttackCurrentTarget()
    {
        if (currentAttackCooldown > 0) return;
        Debug.Log(stats.Damage);
        currentTarget.TakeDamage(stats.Damage);
        currentAttackCooldown = stats.AttackCooldown;
    }

    private void SearchUnitToAttack()
    {
        List<Unit> unitsInRange = unitDetection.UnitsInRange;

        float nearestDistance = float.MaxValue;

        Unit tempTarget = null;

        foreach (Unit unit in unitsInRange)
        {
            if (unit == null) continue;

            if (Vector2.Distance(unit.transform.position, this.transform.position) < nearestDistance)
            {
                tempTarget = unit;
            }
        }

        if (tempTarget != null)
        {
            currentTarget = tempTarget;
        }
    }
}
