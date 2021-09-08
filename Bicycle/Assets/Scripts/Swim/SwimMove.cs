using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimMove : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float gravity;

    //Vector3 player;

    //ector3 Target;

    // Start is called before the first frame update
    void Start() {
       
    }

    // Update is called once per frame
    void FixedUpdate() {
        if(Swim.isWater)
        CommonMove();

        
    }

    private void First() {

    }

    private void CommonMove() {
        /*player = transform.position;
        Target = player + Vector3.forward;*/
        if(Input.GetKeyDown(KeyCode.A)) {
            transform.Translate(new Vector3(0, 1f, 1) * moveSpeed / 20);
            //Vector3.MoveTowards(player, Target, moveSpeed);
           
        }
        else if (Input.GetKeyDown(KeyCode.D)) {
            //Vector3.MoveTowards(player, Target, moveSpeed);

            transform.Translate(new Vector3(0, 1f, 1) * moveSpeed / 20);
        }

    }

}

