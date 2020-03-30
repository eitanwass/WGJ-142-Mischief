using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class InfoShow : MonoBehaviour
{
    public bool active;

    private GameObject info;

    private void Start()
    {
        info = transform.GetChild(0).gameObject;
        info.SetActive(false);
    }

    private void OnMouseEnter()
    {
        info.SetActive(active);
    }

    private void OnMouseExit()
    {
        info.SetActive(false);
    }
}
