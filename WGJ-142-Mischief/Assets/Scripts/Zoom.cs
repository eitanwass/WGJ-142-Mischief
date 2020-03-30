using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class Zoom : MonoBehaviour
{
    public float zoomPeriod;
    public static string neighborhoodName;

    private Vector3 startPos;
    private Vector3 targetPos;
    private float startDistance;
    private float timeLeft = 0;
    private new Camera camera;
    public static bool zoomed;
    private Zoomable zoomOn;

    private void Start()
    {
        camera = GetComponent<Camera>();
        if (camera.orthographic)
            Debug.LogWarning("The camera should be perspective mode for the zoom to work");
        startPos = transform.position;
        zoomOn = null;
    }

    private void Update()
    {
        if (timeLeft > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, startDistance * Time.deltaTime / zoomPeriod);

            timeLeft -= Time.deltaTime;

            if (timeLeft < 0 && zoomOn != null)
                zoomOn.SetActive(true);
        }
        if (!zoomed && Input.GetMouseButtonUp(0))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit) && hit.transform.GetComponent<Zoomable>() != null)
            {
                if (hit.transform.GetComponent<Zoomable>() == null) {
                    return;
                }
                ZoomAt(hit.transform.GetComponent<Zoomable>(), 9f);
            }
        }
        if (zoomed && Input.GetKeyUp(KeyCode.Escape))
            UnZoom();
    }

    public void ZoomAt(Zoomable zoomOn, float zoomScale)
    {
        if (zoomed)
            return;
        this.zoomOn = zoomOn;
        neighborhoodName = zoomOn.name;
        zoomed = true;
        Vector3 zoomOnPos = zoomOn.transform.position;
        targetPos = new Vector3(zoomOnPos.x, zoomOnPos.y, transform.position.z + (zoomOnPos.z - transform.position.z) * (zoomScale - 1) / zoomScale);
        startDistance = Vector3.Distance(transform.position, targetPos);
        timeLeft = zoomPeriod;
    }

    public void ZoomAt(Vector3 position, float zoomScale)
    {
        if (zoomed)
            return;
        zoomed = true;
        targetPos = new Vector3(position.x, position.y, transform.position.z + (position.z - transform.position.z) * (zoomScale - 1) / zoomScale);
        startDistance = Vector3.Distance(transform.position, targetPos);
        timeLeft = zoomPeriod;
    }

    public void UnZoom()
    {
        if (zoomOn != null)
        {
            zoomOn.SetActive(false);
            zoomOn = null;
        }
        zoomed = false;
        targetPos = startPos;
        startDistance = Vector3.Distance(transform.position, targetPos);
        timeLeft = zoomPeriod;
    }
}
