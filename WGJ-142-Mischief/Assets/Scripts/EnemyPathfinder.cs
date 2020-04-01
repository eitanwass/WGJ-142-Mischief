using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

/// <summary>
/// Makes the enemy find a way and move.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Seeker))]
public class EnemyPathfinder : MonoBehaviour
{

    public float speed;
    public float nextWaypointDistance = 3f;

    private Path path;
    int currentWaypoint = 0;

    private Seeker seeker;
    private Rigidbody2D rb;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Takes the enemy to a position.
    /// </summary>
    /// <param name="pos">The position to move to</param>
    /// <param name="scan">Whether we are rescanning the floor before the movement</param>
    public void GoTo(Vector3 pos, bool scan)
    {
        if (scan)
            AstarPath.active.Scan();
        seeker.StartPath(rb.position, pos, OnPathComplete);
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    private void Update()
    {
        if (path == null)
            return;

        if (currentWaypoint >= path.vectorPath.Count)
            return;

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

    }
}
