using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum AwarenessState
{
    UNAWARE,
    SUSPECTING,
    AWARE
}


public class EnemyAI : MonoBehaviour
{
    [Header("References")]
    private Unit pathfinderUnit;

    public GameObject searchTarget;

    private Transform m_currentWalkToTarget;
    public Transform currentWalkToTarget
    {
        get { return m_currentWalkToTarget; }
        set
        {
            m_currentWalkToTarget = value;
            if (pathfinderUnit)
                pathfinderUnit.target = value;
        }
    }


    public AwarenessState awarenessState = AwarenessState.UNAWARE;

    [Header("Vision Cone Settings")]
    public float visionConeAngle = 30f;
    public float visionConeLength = 7.5f;



    private void Awake()
    {
        pathfinderUnit = GetComponent<Unit>();
    }

    private void Update()
    {
        DetectPlayer();
    }


    private void DetectPlayer()
    {
        Vector2 direction = searchTarget.transform.position - this.transform.position;

        float angle = Vector2.Angle(transform.right, direction);

        if (angle <= visionConeAngle)
        {
            float distance = Vector2.Distance(this.transform.position, searchTarget.transform.position);

            if (distance <= visionConeLength)
            {
                RaycastHit2D hit = Physics2D.Raycast(this.transform.position, direction, visionConeLength);

                if (hit.collider != null)
                {
                    Debug.DrawLine(transform.position, hit.point);

                    if (hit.collider.CompareTag("Player"))
                    {
                        Debug.Log("Player in vision");
                    }
                }
            }
        }
    }
}
