using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairs : MonoBehaviour
{
    public float waitTime;
    public Collider2D stairs;
    public GameObject floor1;
    public GameObject floor2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == stairs)
            StartCoroutine(ChangeFloors());
    }

    IEnumerator ChangeFloors()
    {
        yield return new WaitForSecondsRealtime(waitTime);
        floor1.SetActive(!floor1.activeSelf);
        floor2.SetActive(!floor2.activeSelf);
    }
}
