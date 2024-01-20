using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    Navigate,
    Attack,
    NotInitialized
}

public class Enemy : Unit
{
    [SerializeField] protected List<Transform> waypoints = new List<Transform>();

    [SerializeField] protected Rigidbody2D rb;

    protected EnemyState state;

    protected int currentWaypointIndex;

    protected override void Awake()
    {
        base.Awake();
        currentWaypointIndex = 0;
        state = EnemyState.NotInitialized;
    }

    protected virtual void FixedUpdate()
    {
        Move();
    }

    protected virtual void Move()
    {
        Vector2 newPos = Vector2.MoveTowards(this.transform.position, waypoints[currentWaypointIndex].position, stats.MoveSpeed);
        rb.MovePosition(newPos);

        if (transform.position == waypoints[currentWaypointIndex].position)
        {
            if (currentWaypointIndex == waypoints.Count - 1) return;
            currentWaypointIndex++;
        }
    }

    public void SetWaypoints(List<Transform> waypoints)
    {
        this.waypoints = waypoints;
        state = EnemyState.Navigate;
    }

    public void SetPosition(Vector3 position)
    {
        this.transform.position = position;
    }
}
