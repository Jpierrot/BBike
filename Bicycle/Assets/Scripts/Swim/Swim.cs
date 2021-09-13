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

    public GameObject paticle;

    GameObject topPaticle;
    GameObject bottomPaticle;

    bool swim_paticle = false;

    private float swim_time = 0;

    private Color originColor;
    private float originFogDestiniy;

    void Start()
    {
        Debug.Log("렌더링 시작");
        originColor = RenderSettings.fogColor;
        originFogDestiniy = RenderSettings.fogDensity;
        RenderSettings.fog = true;

        bottomPaticle = paticle.transform.GetChild(0).gameObject;
        topPaticle = paticle.transform.GetChild(0).gameObject;

        originDrag = 0;


    }

    // Update is called once per frame
    void Update()
    {
        CheckPaticle();
    }

    private void CheckPaticle() {
        
        if (isWater)
            swim_time += Time.deltaTime;

        if (swim_time > 1f) {
            swim_paticle = false;
            swim_time = 0;
        }

        if (swim_paticle) {
            
            
        }
        else {
            paticle.transform.GetChild(0).gameObject.SetActive(false);
        }

    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            GetWater(other);
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("Player")) {
            
            GetOutWater(other);
        }
    }

    private void GetWater(Collider _player) {
        swim_paticle = true;
        topPaticle.SetActive(true);
        isWater = true;
        _player.transform.GetComponent<Rigidbody>().drag = waterDrag;
        Debug.Log("물나감");
        RenderSettings.fogColor = waterColor;
        RenderSettings.fogDensity = waterFogDenstiy;
    }

    private void GetOutWater(Collider _player) {
        if(isWater) {
            swim_paticle = true;
            bottomPaticle.SetActive(true);
            isWater = false;
            _player.transform.GetComponent<Rigidbody>().drag = originDrag;
            Debug.Log("물 들어옴");
            RenderSettings.fogColor = originColor;
            RenderSettings.fogDensity = originFogDestiniy;
        }

    }
}
