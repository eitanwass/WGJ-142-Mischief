using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GuardAI : EnemyAI
{ 
    
    // Start is called before the first frame update
    void Start()
    {
        //TODO get infromation about the level's multipliers and other information
       this.target = GameObject.FindGameObjectWithTag("Player");
       //this->visionConeAngle = 30f*levelsmultiplier EXAMPLE
    }

    // Update is called once per frame
    void Update()
    {
        EnemyInVision();
    }

    protected override bool EnemyInVision()
    {
        Vector2 direction = target.transform.position - this.transform.position;

        float angle = Vector2.Angle(transform.right, direction);

        if (angle <= this.visionConeAngle)
        {
            float distance = Vector2.Distance(this.transform.position, target.transform.position);

            if (distance <= this.visionConeLength)
            {
                RaycastHit2D hit = Physics2D.Raycast(this.transform.position, direction, visionConeLength);

                if (hit.collider != null)
                {
                    Debug.DrawLine(transform.position, hit.point);

                    if (hit.collider.CompareTag("Player"))
                    {
                        Debug.Log("Player in vision");
                        return true;
                    }
                }
            }
        }
        return false;
    }



}
