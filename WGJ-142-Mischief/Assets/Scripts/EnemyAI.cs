using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum AwarenessState
{
    UNAWARE,
    SUSPECTING,
    AWARE
}

[RequireComponent(typeof(EnemyPathfinder))]
public abstract class EnemyAI : MonoBehaviour
{
    protected GameObject target = null;
    protected EnemyPathfinder enemyPathfinder;

    protected AwarenessState awarenessState = AwarenessState.UNAWARE;

    protected float visionConeAngle = 60f;
    protected float visionConeLength = 7.5f;

    public bool isFollowingPlayer = false;


    protected void Awake()
    {
        enemyPathfinder = GetComponent<EnemyPathfinder>();
        enemyPathfinder.OnPathCompleteEvent += OnPathCompleteEvent();
    }

    protected EventHandler OnPathCompleteEvent()
    {
        isFollowingPlayer = false;
        return null;
    }

    //functions
    protected bool EnemyInVision()
    {
        Vector2 direction = target.transform.position - this.transform.position;

        float angle = Vector2.Angle(transform.right, direction);

        if (angle <= this.visionConeAngle)
        {
            if (IsEnemyInRange())
            {
                RaycastHit2D[] hits = Physics2D.RaycastAll(this.transform.position, direction, visionConeLength);

                foreach (var hit in hits)
                {
                    if (hit.collider != null)
                    {
                        Debug.DrawLine(transform.position, hit.point);

                        if (hit.collider.CompareTag("Player"))
                        {
                            return true;
                        }
                        else
                        {
                            Debug.Log(hit.collider.name);
                        }
                    }
                }
            }
        }
        return false;
    }

    protected bool IsEnemyInRange()
    {
        float distance = Vector2.Distance(this.transform.position, target.transform.position);

        return (distance <= this.visionConeLength);
    }
}
