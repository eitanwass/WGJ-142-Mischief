using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    EnemyPathfinder enemyPathfinder;

    private void Start()
    {
        enemyPathfinder = GetComponent<EnemyPathfinder>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            enemyPathfinder.GoTo(Camera.main.ScreenToWorldPoint(Input.mousePosition), true);
        }
    }
}
