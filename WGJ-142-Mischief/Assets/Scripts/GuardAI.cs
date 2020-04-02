﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GuardAI : EnemyAI
{ 
    void Start()
    {
        //TODO get infromation about the level's multipliers and other information
       this.target = GameObject.FindGameObjectWithTag("Player");
       //this->visionConeAngle = 30f*levelsmultiplier EXAMPLE
    }

    void Update()
    {
        if (EnemyInVision() && !isFollowingPlayer)
        {
            StartCoroutine(GoToPlayer());
            isFollowingPlayer = true;
        }
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
