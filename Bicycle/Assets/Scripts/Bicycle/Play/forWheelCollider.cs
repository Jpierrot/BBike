using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forWheelCollider : MonoBehaviour
{
    public GameObject[] wheelMesh;
    WheelCollider[] wheels = new WheelCollider[2];
    WheelFrictionCurve[] wheelCurves = new WheelFrictionCurve[2];

    public float motorTorque = 200;
    Rigidbody car;
    public float steertingMax = 2;

    public float tempo;


    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 1; i++)
            setupWheelCollider(wheels[i].gameObject, i);
    }

    private void FixedUpdate() {

        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            wheels[1].brakeTorque = 100000f;
            wheels[1].forwardFriction.asymptoteValue = wheelCurves[1].extremumValue;
            wheels[0].motorTorque = 100f;

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

    void animateWheels() {
        Vector3 wheelPosition = Vector3.zero;
        Quaternion wheelRotation = Quaternion.identity;

        for(int i = 0; i < 2; i++) {
            wheels[i].GetWorldPose(out wheelPosition, out wheelRotation);
            wheelMesh[i].transform.position = wheelPosition;
            wheelMesh[i].transform.rotation = wheelRotation;
        }
    }

    public void setupWheelCollider(GameObject element, int i) {
        WheelCollider collider = element.AddComponent<WheelCollider>();
        WheelFrictionCurve curve = collider.forwardFriction;
        JointSpring s = collider.suspensionSpring;

        wheels[i] = collider;
    }

}
