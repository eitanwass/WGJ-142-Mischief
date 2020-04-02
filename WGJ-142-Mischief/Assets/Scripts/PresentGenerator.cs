using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresentGenerator : MonoBehaviour
{
    public int presentAmount = 3;
    private void Awake()
    {
        Present[] presents = FindObjectsOfType<Present>();

        int leftToHide = presents.Length - presentAmount;
        while (leftToHide > 0)
        {
            int num = Random.Range(0, presents.Length);
            if (presents[num].gameObject.activeSelf)
            {
                presents[num].gameObject.SetActive(false);
                leftToHide--;
            }
        }

    }
}
