using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forWheelCollider : MonoBehaviour
{
    public WheelCollider front;
    public WheelCollider back;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Drift() {
        if (Input.GetKeyDown(KeyCode.LeftShift)) {

            if (back.isGrounded) {
                back.brakeTorque = 100000f;
            }
        }
    }
}
