using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neutrophil : ImmuneCell
{
    [SerializeField] private Animator anim;
    [SerializeField] private UnitDetection unitDetection;
    private SpriteRenderer sr;

    private Unit currentTarget;
    private float currentAttackCooldown = 0;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if(coll.enabled)
        {
            SearchUnitToAttack();
        }
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

        anim.SetTrigger("Attack");
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
            if (unit == null || unit is not Enemy) continue;

            if (Vector2.Distance(unit.transform.position, this.transform.position) < nearestDistance)
            {
                tempTarget = unit;
            }
        }

        if (tempTarget != null)
        {
            currentTarget = tempTarget;
            if(tempTarget.transform.position.x < transform.position.x)
            {
                sr.flipX = true;
            }
            else if(tempTarget.transform.position.x > transform.position.x)
            {
                sr.flipX = false;
            }
        }
    }
}
