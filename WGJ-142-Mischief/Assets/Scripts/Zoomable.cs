using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(Collider))]
public class Zoomable : MonoBehaviour
{
    public int zoomLevel;
    [Tooltip("Optional.\nIf not empty loads the scene when zoomed into.")]
    public string scene;

    private bool active;
    private InfoShow[] children;

    void Start()
    {
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
