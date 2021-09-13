using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    public float carSpeed;
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<NavMeshAgent>().speed = carSpeed;
        GetComponent<NavMeshAgent>().SetDestination(target.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
