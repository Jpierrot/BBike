using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove: MonoBehaviour
{
    public float cameraSpeed = 5.0f;

    public GameObject player;

    Vector3 dir;

    private void Start() {
        Vector3 dir = transform.position - player.transform.position;
    }
    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.transform.position + dir;
    }
}
