using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Camera mainCam;

    public GameObject followObject;

    public Vector3 offset = new Vector3(0, 0, -10);

    public float smoothFollowSpeed = 3f;

    private Vector3 currentMousePosition;

    public float cameraMovementRadius = 2f;



    private void Awake()
    {
        mainCam = Camera.main;
    }

    private void Update()
    {
        Vector2 mousePos = Input.mousePosition;
        currentMousePosition = mainCam.ScreenToWorldPoint(mousePos);
        currentMousePosition.z = 0;
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = (followObject.transform.position + currentMousePosition) / 2f;

        // Check if target position is too far
        if(Vector3.Distance(followObject.transform.position, targetPosition) > cameraMovementRadius)
        {
            Vector3 deltaVector = targetPosition - followObject.transform.position;
            deltaVector.Normalize();
            deltaVector *= cameraMovementRadius;
            targetPosition = followObject.transform.position + deltaVector;
        }

        this.transform.position = Vector3.Lerp(transform.position, targetPosition + offset, smoothFollowSpeed * Time.deltaTime);
    }
}
