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
    public GameObject target;


    public AwarenessState awarenessState = AwarenessState.UNAWARE;

    public float visionConeAngle = 30f;

    public float visionConeLength = 7.5f;


    private void Update()
    {
        Vector2 direction = target.transform.position - this.transform.position;

        float angle = Vector2.Angle(transform.right, direction);

        if(angle <= visionConeAngle)
        {
            float distance = Vector2.Distance(this.transform.position, target.transform.position);

            if(distance <= visionConeLength)
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
