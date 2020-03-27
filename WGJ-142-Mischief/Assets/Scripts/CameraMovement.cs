using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject followObject;

    public Vector3 offset;

    public float smoothFollowSpeed = 2f;



    private void LateUpdate()
    {
        Vector3 targetPosition = followObject.transform.position + offset;
        this.transform.position = Vector3.Lerp(transform.position, targetPosition, smoothFollowSpeed * Time.deltaTime);
    }
}
