using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    [SerializeField] private List<Transform> waypoints = new List<Transform>();

    [SerializeField] private float moveDelta;

    [SerializeField] private Rigidbody2D rb;

    private int currentWaypointIndex;

    private void Awake()
    {
        currentWaypointIndex = 0;
    }

    private void FixedUpdate()
    {
        Vector2 newPos = Vector2.MoveTowards(this.transform.position, waypoints[currentWaypointIndex].position, moveDelta);
        rb.MovePosition(newPos);

        if(transform.position == waypoints[currentWaypointIndex].position)
        {
            if (currentWaypointIndex == waypoints.Count - 1) return;
            currentWaypointIndex++;
        }
    }
}
