using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomIn : MonoBehaviour
{
    public Camera cam;
    public Bicycle_movement bicycle;

    private void Start()
    {
        cam.fieldOfView = 80;   
    }
    private void LateUpdate()
    {
        ZoomInOut();
    }
    private void ZoomInOut()
    {
        
        if (bicycle.moveSpeed * 2 > 50f)
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, 55, Time.deltaTime * 1.7f);
        }
        else
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, 80, Time.deltaTime * 1.7f);
        }
    }
}
