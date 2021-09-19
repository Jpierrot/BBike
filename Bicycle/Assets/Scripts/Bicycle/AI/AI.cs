using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    [SerializeField]
    public string Ai_name;

    public float carSpeed;
    public Transform target;
    public int nextTarget;
    public int rap = 0;


    public bool timecheck;
    public float time = 0;



    // Start is called before the first frame update
    void Start()
    {
        timecheck = true;
        target = GameManager.Instance.target[nextTarget];

        GetComponent<NavMeshAgent>().speed = carSpeed;
        StartCoroutine(AI_Move());
    }

    private void Update() {
        if(timecheck && GameManager.Instance.gameEnd == false)
        time += Time.deltaTime;
    }

    IEnumerator AI_Move() {
        bool finish = true;
        GetComponent<NavMeshAgent>().SetDestination(target.position);

        while (finish) {
            float dis = (target.position - transform.position).magnitude;

            if (rap >= 7.5) {
                finish = false;
                timecheck = false;
            }

            if (dis <= 5) {
                nextTarget += 1;
                if (nextTarget >= GameManager.Instance.target.Length) {
                    nextTarget = 0;
                    rap++;
                }
                target = GameManager.Instance.target[nextTarget];
                GetComponent<NavMeshAgent>().SetDestination(target.position);
            }
            yield return null;
        }

    }
}
