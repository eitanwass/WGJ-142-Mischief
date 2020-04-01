using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum AwarenessState
{
    UNAWARE,
    SUSPECTING,
    AWARE
}

public abstract class EnemyAI : MonoBehaviour
{
    protected GameObject target = null;
    protected AwarenessState awarenessState = AwarenessState.UNAWARE;

    protected float visionConeAngle = 30f;
    protected float visionConeLength = 7.5f;

    //functions
    abstract protected bool EnemyInVision();
}

/*public class EnemyAI : MonoBehaviour
{
    public GameObject target;


    public AwarenessState awarenessState = AwarenessState.UNAWARE;

    public float visionConeAngle = 30f;

    public float visionConeLength = 7.5f;

    //functions
    virtual protected bool EnemyInVision();
}
*/
