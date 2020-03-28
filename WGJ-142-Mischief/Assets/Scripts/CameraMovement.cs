using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Camera mainCam;

    public GameObject followObject;

    public Vector3 offset;

    public float smoothFollowSpeed = 2f;

    public Vector3 currentMousePosition;

    public float cameraMovementRadius = 4.5f;



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
        Vector3 targetPosition = followObject.transform.position + offset;

        this.transform.position = Vector3.Lerp(transform.position, targetPosition, smoothFollowSpeed * Time.deltaTime);
    }
}
