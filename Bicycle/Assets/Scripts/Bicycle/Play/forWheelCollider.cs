using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forWheelCollider : MonoBehaviour
{
    public GameObject[] wheelObj = new GameObject[2];
    public WheelCollider[] wheels = new WheelCollider[2];
    /// <summary>
    /// µÞ¹ÙÄû¿ë
    /// 0 => forward, 1 => sideway
    /// </summary>
    public WheelFrictionCurve[] wheelCurves = new WheelFrictionCurve[2];

    /// <summary>
    /// ¸¶Âû·Â º¯µ¿
    /// </summary>
    public float slipRate = 1.0f;
    /// <summary>
    /// Å¸ÀÌ¾î ¸¶Âû°è¼ö
    /// </summary>
    public float handBreakSlipRate = 0.4f;

    public float motorTorque = 200;
    Rigidbody car;
    public float steertingMax = 2;

    public float tempo;


    // Start is called before the first frame update
    void Start() {
        for (int i = 0; i < 1; i++) { 
        setupWheelCollider(wheels[i].gameObject, i);
        wheels[i] = wheelObj[i].GetComponent<WheelCollider>();
    }
        wheelCurves[0] = wheels[0].forwardFriction;
        wheelCurves[1] = wheels[1].sidewaysFriction;


    }

    private void FixedUpdate() {

        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            wheelCurves[0].stiffness = handBreakSlipRate;
            wheels[1].forwardFriction = wheelCurves[0];

            wheelCurves[1].stiffness = handBreakSlipRate;
            wheels[1].sidewaysFriction = wheelCurves[1];
        }

        if (Input.GetKeyDown(KeyCode.W)) {
            for(int i = 0; i < wheels.Length; i++) {
                wheels[i].motorTorque = motorTorque;
            }
        }

        if(Input.GetAxis("Horizontal") != 0) {
            for(int i = 0; i < wheels.Length - 1; i++) {
                wheels[i].steerAngle = Input.GetAxis("Horizontal") * steertingMax;
            }
        }
        else {
            for(int i =0; i < wheels.Length - 2; i++) {
                wheels[i].steerAngle = 0;
            }
        }
            
    }

    public void setupWheelCollider(GameObject element, int i) {
        WheelCollider collider = element.AddComponent<WheelCollider>();
        WheelFrictionCurve curve = collider.forwardFriction;
        JointSpring s = collider.suspensionSpring;

        wheels[i] = collider;
    }

}
