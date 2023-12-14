using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    [SerializeField] private UnitDetection unitDetection;
    private Unit currentTarget;
    private float currentAttackCooldown = 0;

    private void Update()
    {
        SearchUnitToAttack();
    }

    protected override void FixedUpdate()
    {
        currentAttackCooldown -= Time.deltaTime;

        switch(state)
        {
            case EnemyState.Attack:
                AttackCurrentTarget();
                break;
            case EnemyState.Navigate:
                Move();
                break;
            default:
                Move();
                break;
        }
    }

    private void AttackCurrentTarget()
    {
        if (currentTarget == null) state = EnemyState.Navigate;

        if (currentAttackCooldown > 0) return;

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

            if(Vector2.Distance(unit.transform.position, this.transform.position) < nearestDistance)
            {
                tempTarget = unit;
            }
        }

        if(tempTarget != null)
        {
            currentTarget = tempTarget;
            state = EnemyState.Attack;
        }
    }
}
