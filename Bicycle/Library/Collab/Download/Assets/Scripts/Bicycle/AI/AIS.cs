using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIS : MonoBehaviour
{ 

    [SerializeField]
    public string Ai_name;

    public float carSpeed;
    public Transform target;
    public int nextTarget;
    public int rap = 0;

    public float time;

    public bool timecheck;

    // Start is called before the first frame update
    void Start()
    {
        timecheck = true;
        target = GameManager.Instance.targets[nextTarget];

        GetComponent<NavMeshAgent>().speed = carSpeed;
        

        StartCoroutine(AI_Move());
    }

    private void FixedUpdate() {
        if (timecheck &&  GameManager.Instance.gameEnd == false)
            time += Time.fixedDeltaTime;
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

            if (dis <= 7.5) {
                nextTarget += 1;
                if (nextTarget >= GameManager.Instance.targets.Length) {
                    nextTarget = 0;
                    rap++;
                }
                target = GameManager.Instance.targets[nextTarget];
                GetComponent<NavMeshAgent>().SetDestination(target.position);
            }
            yield return null;
        }

    }
}
