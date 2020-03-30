using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Camera))]
public class ZoomIntoHouse : MonoBehaviour
{
    public float zoomPeriod;
    public bool canZoom = false;
    public static string houseName;
    
    private Vector3 startPos;
    private Vector3 targetPos;
    private float startDistance;
    private float timeLeft = 0;
    private new Camera camera;
    private bool zoomed;
    private ZoomableHouse zoomOn;

    void Start()
    {
        camera = GetComponent<Camera>();
        if (camera.orthographic)
        Debug.LogWarning("The camera should be perspective mode for the zoom to work");
        startPos = transform.position;
        zoomOn = null;
    }

    void Update()
    {
        // If not zoomed in to the neighborhood, don't do anything
        canZoom = Zoom.zoomed;
        if (!canZoom) {
            return;
        }
        
        // If in the middle of zooming
        if (timeLeft > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, startDistance * Time.deltaTime / zoomPeriod);
            
            timeLeft -= Time.deltaTime;
            
            // If done zooming
            if (timeLeft <= 0) {
                Debug.Log("Going to loading screen");
                SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 1);
            }
            
        }
        
        // If we just clicked on the neighborhood, we will begin zooming
        if (!zoomed && Input.GetMouseButtonUp(0))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            
            // If ray from camera to mouse hits a hosue on the way
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                zoomOn = hit.transform.GetComponent<ZoomableHouse>();
                houseName = zoomOn.name;

                // If there's a problem fetching the house, quit to avoid a crash
                if (zoomOn == null) {
                    return;
                }
                
                ZoomAt(9f);
            }
        }
    }
    
    public void ZoomAt(float zoomScale)
    {
        if (zoomed)
            return;

        zoomed = true;
        
        Vector3 zoomOnPos = zoomOn.transform.position;
        
        targetPos = new Vector3(zoomOnPos.x, zoomOnPos.y, transform.position.z + (zoomOnPos.z - transform.position.z) * (zoomScale - 1) / zoomScale);
        
        startDistance = Vector3.Distance(transform.position, targetPos);
        timeLeft = zoomPeriod;
    }
    
}
