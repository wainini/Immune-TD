using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitDetection : MonoBehaviour
{
    [field: SerializeField] public List<Unit> UnitsInRange { get; private set; } = new List<Unit>();

    private Unit unit;

    [SerializeField] private float detectionRadius = 1.5f;
    [SerializeField] private Vector2 detectionOffset = Vector2.zero;
    [SerializeField] private LayerMask unitLayer;
    [SerializeField] private bool lockGizmos = false;

    private void Update()
    {
        DetectUnitInRange();
    }
    
    private void DetectUnitInRange()
    {
        List<Unit> tempUnitsInRange = new List<Unit>();

        Collider2D[] detectedUnitsInRange = Physics2D.OverlapCircleAll(this.transform.position + (Vector3)detectionOffset, detectionRadius, unitLayer);
        foreach(Collider2D coll in detectedUnitsInRange)
        {
            if (coll.TryGetComponent(out Unit u))
            {
                if (u != unit)
                    tempUnitsInRange.Add(u);
            }
        }
        UnitsInRange = tempUnitsInRange;
    }

    public void SetUnit(Unit unit)
    {
        if(unit != null)
            this.unit = unit;
    }

    private void OnDrawGizmos()
    {
        if (!lockGizmos) return;
        OnDrawGizmosSelected();
    }

    private void OnDrawGizmosSelected()
    {
        UnityEditor.Handles.color = Color.red;

        UnityEditor.Handles.DrawWireDisc(this.transform.position + (Vector3)detectionOffset, Vector3.back, detectionRadius);

        Gizmos.color = Color.green;

        foreach(Unit u in UnitsInRange)
        {
            Gizmos.DrawRay(this.transform.position, u.transform.position - this.transform.position);
        }
    }
}
