using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimMove : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float gravity;

    Vector3 player;

    public GameObject target;

    Vector3 Direction;

    // Start is called before the first frame update
    void Start() {
        player = transform.position;
        Direction = target.transform.position;
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
        if(Input.GetKeyDown(KeyCode.RightArrow)) {
            transform.Translate(new Vector3(0, 0.5f, 1) * moveSpeed);
            
           
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            //Vector3.MoveTowards(player, Target, moveSpeed);
         
            transform.Translate(new Vector3(0, 0.5f, 1) * moveSpeed);
        }

    }

}

