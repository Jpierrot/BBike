using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    public float carSpeed;
    public Transform target;
    int nextTarget;

    // Start is called before the first frame update
    void Start()
    {
        target = GameManager.instance.target[nextTarget];

        GetComponent<NavMeshAgent>().speed = carSpeed;
        GetComponent<NavMeshAgent>().SetDestination(target.position);

        StartCoroutine(AI_Move());
    }

    IEnumerator AI_Move() {
        GetComponent<NavMeshAgent>().SetDestination(target.position);

        while(true) {
            float dis = (target.position - transform.position).magnitude;

            if(dis <= 1) {
                nextTarget += 1;
                if(nextTarget >= GameManager.instance.target.Length) {
                    nextTarget = 0;
                }
                target = GameManager.instance.target[nextTarget];
                GetComponent<NavMeshAgent>().SetDestination(target.position);
            }
            yield return null;
        }
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
