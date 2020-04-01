using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Camera))]
public class Zoom : MonoBehaviour
{
    public const int startLevel = 0;
    public const int neighborhoodLevel = 1;
    public const int houseLevel = 2;

    public float zoomPeriod;
    public float zoomScale;

    private Vector3 startPos;
    private Vector3 targetPos;
    private float startDistance;
    private float timeLeft = 0;
    private new Camera camera;
    private int zoomLevel;
    private Zoomable zoomOn;

    private void Start()
    {
        camera = GetComponent<Camera>();
        if (camera.orthographic)
            Debug.LogWarning("The camera should be perspective mode for the zoom to work");
        startPos = transform.position;
        zoomOn = null;
        zoomLevel = 0;
    }

    private void Update()
    {
        if (timeLeft > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, startDistance * Time.deltaTime / zoomPeriod);

            timeLeft -= Time.deltaTime;

            if (timeLeft < 0 && zoomOn != null)
            {
                zoomOn.SetActive(true);
                if (zoomOn.scene != "")
                {
                    SceneManager.LoadScene(zoomOn.scene);
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            RaycastHit[] hit = Physics.RaycastAll(ray);

            foreach (var target in hit)
            {
                Zoomable zoomAtObj = target.transform.GetComponent<Zoomable>();
                if (zoomLevel + 1 == zoomAtObj.zoomLevel)
                {
                    ZoomAt(zoomAtObj, zoomScale);
                    break;
                }
            }
        }
        if (zoomLevel > 0 && Input.GetKeyUp(KeyCode.Escape))
            UnZoom();
    }

    public void ZoomAt(Zoomable zoomOn, float scale)
    {
        if (zoomLevel == houseLevel)
            return;
        this.zoomOn = zoomOn;
        zoomLevel++;
        Vector3 zoomOnPos = zoomOn.transform.position;
        targetPos = new Vector3(zoomOnPos.x, zoomOnPos.y, transform.position.z + (zoomOnPos.z - transform.position.z) * (scale - 1) / scale);
        startDistance = Vector3.Distance(transform.position, targetPos);
        timeLeft = zoomPeriod;
    }


    public void ZoomAt(Vector3 position, float scale)
    {
        if (zoomLevel == houseLevel)
            return;
        zoomLevel++;
        targetPos = new Vector3(position.x, position.y, transform.position.z + (position.z - transform.position.z) * (scale - 1) / scale);
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
        zoomLevel = startLevel;
        targetPos = startPos;
        startDistance = Vector3.Distance(transform.position, targetPos);
        timeLeft = zoomPeriod;
    }
}
