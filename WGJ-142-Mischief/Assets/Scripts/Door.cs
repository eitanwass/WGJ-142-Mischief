using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactble
{
    public GameObject door;

    // Open/close a door.
    public override void Interact()
    {
        door.SetActive(!door.activeSelf);
    }
}
