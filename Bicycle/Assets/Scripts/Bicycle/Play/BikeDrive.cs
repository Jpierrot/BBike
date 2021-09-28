using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BikeDrive : MonoBehaviour
{
    public List<forWheelCollider> wheel;
    public float MaxMotorTorque;
    public float MaxSteeringAngle;
    public float breakTorque;
    public float decelrationForce;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*public void ApplyLocalPositionToVisuals(forWheelCollider wheel) {
        Vector3 position;
        Quaternion rotation;
        wheel.front.GetWorldPose(out position, out rotation);
    }*/

    private void FixedUpdate() {


    }
}
