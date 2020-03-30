using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class Interactble : MonoBehaviour
{
    public abstract void Interact();
}
