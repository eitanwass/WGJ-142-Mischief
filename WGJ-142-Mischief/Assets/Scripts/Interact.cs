using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Interact : MonoBehaviour
{
    public KeyCode useKey;
    private Interactble obj = null;
    public TextMeshProUGUI text;

    private void Update()
    {
        if (Input.GetKeyUp(useKey) && obj != null)
        {
            obj.Interact();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Interactble>() != null)
        {
            obj = collision.GetComponent<Interactble>();
            text.text = "Press " + useKey + " to interact with " + obj.name;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (obj == null && collision.GetComponent<Interactble>() != null)
        {
            obj = collision.GetComponent<Interactble>();
            text.text = "Press " + useKey + " to interact with " + obj.name;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Interactble>() == obj)
        {
            obj = null;
            text.text = "";
        }
    }
}
