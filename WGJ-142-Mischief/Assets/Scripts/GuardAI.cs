using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardAI : EnemyAI
{
    public float hearDistance = 2f;
    public float wanderRange;

    public bool wandering = false;

    void Start()
    {
        //TODO get infromation about the level's multipliers and other information
        target = GameObject.FindGameObjectWithTag("Player");
        //this->visionConeAngle = 30f*levelsmultiplier EXAMPLE
        //Debug.Log(ProgressHandler.levelsInfo[0, 0].bestTime);
        enemyPathfinder.OnPathCompleteEvent.AddListener(DoneWander);
    }

    void Update()
    {
        if (!isFollowingPlayer)
        {
            if (EnemyInVision() || Vector3.Distance(transform.position, target.transform.position) < hearDistance)
            {
                StartCoroutine(GoToPlayer());
                awarenessState = AwarenessState.AWARE;
                isFollowingPlayer = true;
            }
            else if (!wandering)
            {
                print("new road");
                float x = UnityEngine.Random.Range(transform.position.x - wanderRange, transform.position.x + wanderRange);
                float y = UnityEngine.Random.Range(transform.position.y - wanderRange, transform.position.y + wanderRange);
                enemyPathfinder.GoTo(new Vector3(x, y));
                wandering = true;
            }
        }
        
    }

    protected void DoneWander()
    {
        wandering = false;
    }

    IEnumerator GoToPlayer()
    {
        while (IsEnemyInRange())
        {
            enemyPathfinder.GoTo(target.transform.position);
            yield return new WaitForSecondsRealtime(1f);
        }
        isFollowingPlayer = false;
    }
}
