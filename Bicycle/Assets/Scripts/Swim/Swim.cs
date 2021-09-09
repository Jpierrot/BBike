using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Swim : MonoBehaviour
{
    public static bool isWater = false;

    [SerializeField] private float waterDrag;
    private float originDrag;

    [SerializeField] private Color waterColor;
    [SerializeField] private float waterFogDenstiy;

    public GameObject camera;

    private float swim_time = 0;

    private Color originColor;
    private float originFogDestiniy;

    void Start()
    {
        Debug.Log("렌더링 시작");
        originColor = RenderSettings.fogColor;
        originFogDestiniy = RenderSettings.fogDensity;
        RenderSettings.fog = true;

        originDrag = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isWater)
            swim_time += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            GetWater(other);
            
            camera.transform.GetChild(0).gameObject.SetActive(false);
            camera.transform.GetChild(1).gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("Player")) {
            camera.transform.GetChild(0).gameObject.SetActive(true);
            camera.transform.GetChild(1).gameObject.SetActive(true);
            GetOutWater(other);
        }
    }

    private void GetWater(Collider _player) {
        isWater = true;
        _player.transform.GetComponent<Rigidbody>().drag = waterDrag;
        Debug.Log("물나감");
        RenderSettings.fogColor = waterColor;
        RenderSettings.fogDensity = waterFogDenstiy;
    }

    private void GetOutWater(Collider _player) {
        if(isWater) {
            isWater = false;
            _player.transform.GetComponent<Rigidbody>().drag = originDrag;
            Debug.Log("물 들어옴");
            RenderSettings.fogColor = originColor;
            RenderSettings.fogDensity = originFogDestiniy;
        }

    }
}
