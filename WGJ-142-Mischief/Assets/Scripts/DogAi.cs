using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogAi : EnemyAI
{
    void Start()
    {
        //TODO get infromation about the level's multipliers and other information
        this.target = GameObject.FindGameObjectWithTag("Player");
        this.visionConeAngle = 360f;
        //this->visionConeAngle = 30f*levelsmultiplier EXAMPLE
    }


    void Update()
    {
        if (EnemyInVision())
        {
            enemyPathfinder.GoTo(target.transform.position, !isFollowingPlayer);
            isFollowingPlayer = true;
        }
    }
}
