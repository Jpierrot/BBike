using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    [SerializeField]
    private string Ai_name;

    public float carSpeed;
    public Transform target;
    int nextTarget;
    int rap = 0;

    bool timecheck;

    float time = 0;



    // Start is called before the first frame update
    void Start()
    {
        target = GameManager.instance.target[nextTarget];

        GetComponent<NavMeshAgent>().speed = carSpeed;
        GetComponent<NavMeshAgent>().SetDestination(target.position);

        StartCoroutine(AI_Move());
    }

    private void Update() {
        if(timecheck)
        time += Time.deltaTime;
    }

    IEnumerator AI_Move() {
        bool finish = true;
        GetComponent<NavMeshAgent>().SetDestination(target.position);

        while (finish) {
            float dis = (target.position - transform.position).magnitude;

            if (rap >= 3) {
                finish = false;
                timecheck = false;
            }

            if (dis <= 5) {
                nextTarget += 1;
                if (nextTarget >= GameManager.instance.target.Length) {
                    nextTarget = 0;
                    rap++;
                }
                target = GameManager.instance.target[nextTarget];
                GetComponent<NavMeshAgent>().SetDestination(target.position);
            }
            yield return null;
        }

    }
}
