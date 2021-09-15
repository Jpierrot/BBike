using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    [SerializeField]
    private GameObject bike;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision) 
    {


    }

    private void OnTriggerEnter(Collider other) 
    {
        
            Debug.Log("ÆÄ±«µÊ");
        Destroy(bike, 0.5f);
    }


}
