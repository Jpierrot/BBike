using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideNav : MonoBehaviour
{
    public GameObject navi;
    public Camera cam;  
    void Start()
    {
        NavCheck(8);
    }
    void NavCheck(int layerindex)
    {
        cam.cullingMask = ~(8 << layerindex);
    }

}
