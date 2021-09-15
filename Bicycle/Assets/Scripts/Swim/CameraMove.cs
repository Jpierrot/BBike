using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove: MonoBehaviour
{
    public float cameraSpeed;

    public GameObject player;

    Vector3 camera;


    private void Start() {
      
    }
    // Update is called once per frame
    void LateUpdate()
    {
        camera = new Vector3(player.transform.position.x, 30, player.transform.position.z);
        transform.eulerAngles = new Vector3(90, 0, 360 - player.transform.eulerAngles.y); 
        transform.position = camera;
    }
}
