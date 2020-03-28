using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Zoomable : MonoBehaviour
{
    private bool active;
    private InfoShow[] children;
    void Start()
    {
        gameObject.tag = "Zoomable";
        children = GetComponentsInChildren<InfoShow>();
        SetActive(false);
    }

    public void SetActive(bool value)
    {
        active = value;
        foreach (var item in children)
        {
            item.active = active;
        }
    }
}
